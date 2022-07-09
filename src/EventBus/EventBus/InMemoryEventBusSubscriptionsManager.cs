using EventBus.Abstractions;
using EventBus.Events;

namespace EventBus;

public class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
{
    public InMemoryEventBusSubscriptionsManager()
    {
        _handlers = new Dictionary<string, List<SubscriptionInfo>>();
        _eventTypes = new List<Type>();
    }
    
    private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
    
    private readonly List<Type> _eventTypes;
    
    public bool IsEmpty => _handlers.Count == 0;
    
    public void Clear() => _handlers.Clear();

    public event EventHandler<string> OnEventRemoved;

    private void DoAddSubscription(Type handlerType, string eventName)
    {
        if (!HasSubscriptionForEvenet(eventName))
        {
            _handlers.Add(eventName,new List<SubscriptionInfo>());
        }

        if (_handlers[eventName].Any(s => s.HandleType == handlerType))
        {
            throw new ArgumentException(
                $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
        }
        _handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
    }
    public void AddSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        var eventName = GetEventKey<T>();
        
        DoAddSubscription(typeof(TH), eventName);
        
        if (!_eventTypes.Contains(typeof(T)))
        {
            _eventTypes.Add(typeof(T));
        }
        
    }

    public void RemoveSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        throw new NotImplementedException();
    }

    public bool HasSubscriptionForEvent<T>() where T : IntegrationEvent
    {
        throw new NotImplementedException();
    }

    public bool HasSubscriptionForEvenet(string eventName)
    {
       return _handlers.ContainsKey(eventName);
    }

    public Type GetEventTypeByName(string eventName)
    {
        throw new NotImplementedException();
    }



    public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
    {
        throw new NotImplementedException();
    }

    public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>(string eventName)
    {
        throw new NotImplementedException();
    }

    public string GetEventKey<T>()
    {
       return typeof(T).Name;
    }
}