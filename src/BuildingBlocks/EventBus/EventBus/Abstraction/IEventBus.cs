using EventBus.Events;

namespace EventBus.Abstraction
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent integrationEvent);

        void Subscribe<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler<TEvent>;

        void Unsubscribe<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler<TEvent>;
    }
}