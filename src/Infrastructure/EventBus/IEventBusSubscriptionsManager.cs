using EventBus.Abstractions;
using EventBus.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus
{
    interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }
        event EventHandler<string> OnEventRemoved;

        void AddDynamicSubscription<TH>(string eventName) where TH : IDynamicIntegrationEventHandler;
        void RemoveDynamicSubscription<TH>(string eventName) where TH : IDynamicIntegrationEventHandler;

        void AddSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
        void RemoveSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;

        bool HasSubscriptionForEvent<T>() where T : IntegrationEvent;
        bool HasSubscriptionForEvent(string eventName);

        Type GetEventTypeByName(string eventName);
        void Clear();

        IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);

        string GetEventKey<T>();
    }
}
