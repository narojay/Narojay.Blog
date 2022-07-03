using EventBus.Abstractions;
using EventBus.Events;

namespace EventBus;

public interface IEventBusSubscriptionsManager
{
    bool IsEmpty { get; }

    event EventHandler<string> OnEventRemoved; 
    void AddSubscription<T, TH>() 
        where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;

    void RemoveSubscription<T, TH>() 
        where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;

    bool HasSubscriptionForEvent<T>() where T : IntegrationEvent;

    bool HasSubscriptionForEvenet(string eventName);

    Type GetEventTypeByName(string eventName);

    void Clear();
    
    IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;

    IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>(string eventName);

    string GetEventKey<T>();





}