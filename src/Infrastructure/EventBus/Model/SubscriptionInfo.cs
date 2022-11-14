using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Model
{
    public class SubscriptionInfo
    {
        public bool IsDynamic { get; }
        public Type HandlerType { get; }

        public SubscriptionInfo(bool isDynamic, Type handlerType)
        {
            this.IsDynamic = isDynamic;
            this.HandlerType = handlerType;
        }

        public static SubscriptionInfo Dynamic(Type handlerType)
        {
            return new SubscriptionInfo(true, handlerType);
        }
        public static SubscriptionInfo Typed(Type handlerType)
        {
            return new SubscriptionInfo(false, handlerType);
        }
    }
}
