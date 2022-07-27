using System;

namespace EventBus;

public class SubscriptionInfo
{
    public SubscriptionInfo(bool isDynamic, Type handleType)
    {
        IsDynamic = isDynamic;
        HandleType = handleType;
    }

    public Type HandleType { get; }

    public bool IsDynamic { get; }

    public static SubscriptionInfo Dynamic(Type handlerType)
    {
        return new SubscriptionInfo(true, handlerType);
    }

    public static SubscriptionInfo Typed(Type handlerType)
    {
        return new SubscriptionInfo(false, handlerType);
    }
}