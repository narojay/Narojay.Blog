using System.Text.Json;
using EventBus;
using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace EventBusRabbitMQ;

public class EventBusRabbitMQ : IEventBus
{
   public const  string   BROKER_NAME = "narojay_blog_event_bus";
   private readonly IRabbitMQPersistentConnection _connection;
    private readonly IEventBusSubscriptionsManager _busSubscriptionsManager;
    private readonly ILogger<EventBusRabbitMQ> _logger;
    private IModel _consumerChannel;

    public EventBusRabbitMQ(IRabbitMQPersistentConnection connection,
        IEventBusSubscriptionsManager busSubscriptionsManager, ILogger<EventBusRabbitMQ> logger)
    {
        _connection = connection;
        _busSubscriptionsManager = busSubscriptionsManager;
        _logger = logger;
        _consumerChannel = CreateConsumerChannel();
    }

    private IModel CreateConsumerChannel()
    {
        if (!_connection.IsConnected)
        {
            _connection.TryConnect();
        }

        _logger.LogTrace("Creating RabbitMQ consumer channel");

        var channel = _connection.CreateModel();
        channel.ExchangeDeclare(exchange: BROKER_NAME,
            ExchangeType.Direct, true);

        return channel;
    }

    public void Publish(IntegrationEvent @event)
    {
        if (!_connection.IsConnected)
        {
            _connection.TryConnect();
        }

        var eventName = @event.GetType().Name;

        _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, eventName);

        using (var channel = _connection.CreateModel())
        {
            _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);


            var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), new JsonSerializerOptions
            {
                WriteIndented = true
            });
            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2; // persistent

            _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.Id);
            channel.BasicPublish(
                exchange: BROKER_NAME,
                routingKey: eventName,
                mandatory: false,
                basicProperties: properties,
                body: body);
        }
    }

    private void DoInternalSubscription(string eventName)
    {
        var containsKey = _busSubscriptionsManager.HasSubscriptionForEvenet(eventName);
        if (!containsKey)
        {
            if (!_connection.IsConnected)
            {
                _connection.TryConnect();
            }
        }
    }

    public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        var eventName = _busSubscriptionsManager.GetEventKey<T>();
        DoInternalSubscription(eventName);
        _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).Name);
        _busSubscriptionsManager.AddSubscription<T, TH>();
    }

    public void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        var eventName = _busSubscriptionsManager.GetEventKey<T>();

        _logger.LogInformation("Unsubscribing from event {EventName}", eventName);

        _busSubscriptionsManager.RemoveSubscription<T, TH>();
    }
}