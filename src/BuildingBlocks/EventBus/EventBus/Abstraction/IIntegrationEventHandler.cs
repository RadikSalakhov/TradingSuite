using EventBus.Events;

namespace EventBus.Abstraction
{
    public interface IIntegrationEventHandler
    {
    }

    public interface IIntegrationEventHandler<TEvent> : IIntegrationEventHandler
        where TEvent : IntegrationEvent
    {
        Task Handle(TEvent integrationEvent);
    }
}