using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public class SubscriptionInfo
    {
        public Type HandleType { get;  }

        public bool IsDynamic { get; }

        public SubscriptionInfo(bool isDynamic,Type handleType)
        {
            IsDynamic = isDynamic;
            HandleType = handleType;

        }
        public static SubscriptionInfo Dynamic(Type handlerType) => new(true, handlerType);

        public static SubscriptionInfo Typed(Type handlerType) => new(false, handlerType);
    }
}
