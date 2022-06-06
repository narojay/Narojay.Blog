using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventBusRabbitMQ;

public class DefaultRabbitMQPersistentConnection : IRabbitMQPersistentConnection, IDisposable
{
    private readonly IConnectionFactory _connectionFactory;
    private IConnection _connection;
    private IModel _consumerChannel;

    public DefaultRabbitMQPersistentConnection(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        //_consumerChannel = CreateConsumerChannel();
        //BindBasicConsumer();
    }


    public void Dispose()
    {
        _consumerChannel.Dispose();
        _connection.Dispose();
    }

    public bool IsConnected => _connection != null && _connection.IsOpen;

    public bool TryConnect()
    {
        _connection = _connectionFactory.CreateConnection();
        if (IsConnected)
            return true;
        return false;
    }

    public IModel CreateModel()
    {
        TryConnect();
        if (!IsConnected)
            throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");

        return _connection.CreateModel();
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
        catch (Exception ex)
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