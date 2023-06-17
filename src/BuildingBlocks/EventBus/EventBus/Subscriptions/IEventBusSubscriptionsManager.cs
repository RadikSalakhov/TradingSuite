using EventBus.Abstraction;
using EventBus.Events;

namespace EventBus.Subscriptions
{
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnEventRemoved;

        void AddSubscription<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler<TEvent>;

        void RemoveSubscription<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler<TEvent>;

        bool HasSubscriptionsForEvent<TEvent>()
            where TEvent : IntegrationEvent;

        bool HasSubscriptionsForEvent(string eventName);

        Type? GetEventTypeByName(string eventName);

        void Clear();

        IEnumerable<SubscriptionInfo> GetHandlersForEvent<TEvent>()
            where TEvent : IntegrationEvent;

        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);

        string GetEventKey<T>();
    }
}