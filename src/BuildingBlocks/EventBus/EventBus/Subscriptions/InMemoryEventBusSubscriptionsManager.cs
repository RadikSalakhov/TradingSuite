using EventBus.Abstraction;
using EventBus.Events;

namespace EventBus.Subscriptions
{
    public class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers = new Dictionary<string, List<SubscriptionInfo>>();
        private readonly List<Type> _eventTypes = new List<Type>();

        public event EventHandler<string>? OnEventRemoved;

        public InMemoryEventBusSubscriptionsManager()
        {
        }

        public bool IsEmpty => !_handlers.Any();

        public void Clear() => _handlers.Clear();

        public string GetEventKey<T>()
        {
            return typeof(T).Name;
        }

        public Type? GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName);

        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

        public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return HasSubscriptionsForEvent(key);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) => _handlers[eventName];

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return GetHandlersForEvent(key);
        }

        public void AddSubscription<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler<TEvent>
        {
            var eventName = GetEventKey<TEvent>();

            DoAddSubscription(typeof(THandler), eventName);

            if (!_eventTypes.Contains(typeof(TEvent)))
                _eventTypes.Add(typeof(TEvent));
        }

        public void RemoveSubscription<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent
        {
            var handlerToRemove = FindSubscriptionToRemove<T, TH>();
            var eventName = GetEventKey<T>();
            DoRemoveHandler(eventName, handlerToRemove);
        }

        private void DoAddSubscription(Type handlerType, string eventName)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                _handlers.Add(eventName, new List<SubscriptionInfo>());
            }

            if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
            }

            _handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
        }

        private void DoRemoveHandler(string eventName, SubscriptionInfo? subsToRemove)
        {
            if (subsToRemove == null)
                return;

            _handlers[eventName].Remove(subsToRemove);
            if (!_handlers[eventName].Any())
            {
                _handlers.Remove(eventName);
                var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
                if (eventType != null)
                {
                    _eventTypes.Remove(eventType);
                }
                RaiseOnEventRemoved(eventName);
            }
        }

        private void RaiseOnEventRemoved(string eventName)
        {
            var handler = OnEventRemoved;
            handler?.Invoke(this, eventName);
        }

        private SubscriptionInfo? FindSubscriptionToRemove<T, TH>()
                where T : IntegrationEvent
                where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();
            return DoFindSubscriptionToRemove(eventName, typeof(TH));
        }

        private SubscriptionInfo? DoFindSubscriptionToRemove(string eventName, Type handlerType)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                return null;
            }

            return _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);
        }
    }
}