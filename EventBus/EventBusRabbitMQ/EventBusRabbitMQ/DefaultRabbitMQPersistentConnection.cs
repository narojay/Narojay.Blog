using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventBusRabbitMQ
{
    public class DefaultRabbitMQPersistentConnection :IRabbitMQPersistentConnection,IDisposable
    {
        private readonly IConnectionFactory _connectionFactory;
        private IModel _consumerChannel;
        public DefaultRabbitMQPersistentConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            //_consumerChannel = CreateConsumerChannel();
            //BindBasicConsumer();
        }
        private IConnection _connection;

        public bool IsConnected => _connection != null && _connection.IsOpen;

        public bool TryConnect()
        {
            _connection= _connectionFactory.CreateConnection();
            if (IsConnected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void BindBasicConsumer()
        {

            if (_consumerChannel != null)
            {
                var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                consumer.Received += Consumer_Received;

                _consumerChannel.BasicConsume(
                    queue: "test_queue",
                    autoAck: false,
                    consumer: consumer);
            }
            else
            {
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
            _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }

        private IModel CreateConsumerChannel()
        {
            if (!IsConnected)
            {
                TryConnect();
            }


            var channel = _connection.CreateModel();

            channel.ExchangeDeclare(exchange: "narojay_blog_exchange",
                type: "direct");

            channel.QueueDeclare(queue: "test_queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            channel.BasicQos(0,50,false);

            return channel;
        }
        public IModel CreateModel()
        {
            TryConnect();
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }

            return _connection.CreateModel();
        }


        public void Dispose()
        {
            _consumerChannel.Dispose();
            _connection.Dispose();
        }
    }
}
