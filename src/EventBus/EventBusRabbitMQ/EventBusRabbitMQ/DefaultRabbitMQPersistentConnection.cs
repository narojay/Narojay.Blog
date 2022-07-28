using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventBusRabbitMQ;

public class DefaultRabbitMQPersistentConnection : IRabbitMQPersistentConnection, IDisposable
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly ILogger<DefaultRabbitMQPersistentConnection> _logger;
    private IConnection _connection;
    private IModel _consumerChannel;
    private bool _disposed;

    public DefaultRabbitMQPersistentConnection(IConnectionFactory connectionFactory,
        ILogger<DefaultRabbitMQPersistentConnection> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
        //_consumerChannel = CreateConsumerChannel();
        //BindBasicConsumer();
    }


    public void Dispose()
    {
        if (_disposed) return;

        _disposed = true;

        try
        {
            _connection.Dispose();
        }
        catch (IOException ex)
        {
            _logger.LogWarning(ex.ToString());
        }
    }

    public bool IsConnected => _connection != null && _connection.IsOpen;

    public bool TryConnect()
    {
        if (!IsConnected)
        {
            _connection = _connectionFactory.CreateConnection();
            return false;
        }

        if (IsConnected)
        {
            _connection.ConnectionShutdown += OnConnectionShutdown;
            _connection.CallbackException += OnCallbackException;
            _connection.ConnectionBlocked += OnConnectionBlocked;

            Console.WriteLine(
                $"RabbitMQ persistent connection acquired a connection {_connection.Endpoint.HostName} and is subscribed to failure events");

            return true;
        }

        return false;
    }

    public IModel CreateModel()
    {
        TryConnect();
        if (!IsConnected)
            throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");

        return _connection.CreateModel();
    }

    private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
    {
        if (_disposed) return;
        _logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect...");
        TryConnect();
    }

    private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
    {
        if (_disposed) return;
        _logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect...");
        TryConnect();
    }

    private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
    {
        if (_disposed) return;
        _logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect...");
        TryConnect();
    }

    public void BindBasicConsumer()
    {
        if (_consumerChannel != null)
        {
            var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

            consumer.Received += Consumer_Received;

            _consumerChannel.BasicConsume(
                "test_queue",
                false,
                consumer);
        }
    }

    private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
    {
        var eventName = eventArgs.RoutingKey;
        try
        {
            var message = Encoding.UTF8.GetString(eventArgs.Body.Span);
            Console.WriteLine(message);
            await Task.Delay(1);
        }
        catch (Exception)
        {
            Console.WriteLine("失败");
        }

        // Even on exception we take the message off the queue.
        // in a REAL WORLD app this should be handled with a Dead Letter Exchange (DLX). 
        // For more information see: https://www.rabbitmq.com/dlx.html
        _consumerChannel.BasicAck(eventArgs.DeliveryTag, false);
    }

    private IModel CreateConsumerChannel()
    {
        if (!IsConnected) TryConnect();


        var channel = _connection.CreateModel();

        channel.ExchangeDeclare("narojay_blog_exchange",
            "direct");

        channel.QueueDeclare("test_queue",
            false,
            false,
            false,
            null);
        channel.BasicQos(0, 50, false);

        return channel;
    }
}