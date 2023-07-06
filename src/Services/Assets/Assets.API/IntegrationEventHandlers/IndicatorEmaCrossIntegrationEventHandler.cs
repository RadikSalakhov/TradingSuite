using Assets.API.IntegrationEvents;
using EventBus.Abstraction;
using Microsoft.AspNetCore.SignalR;

namespace Assets.API.IntegrationEventHandlers
{
    public class IndicatorEmaCrossIntegrationEventHandler : IIntegrationEventHandler<IndicatorEmaCrossIntegrationEvent>
    {
        public IndicatorEmaCrossIntegrationEventHandler()
        {
        }

        public async Task Handle(IndicatorEmaCrossIntegrationEvent integrationEvent)
        {
            //using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new("IntegrationEventContext", integrationEvent.Id) }))
            //{
            //    _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", integrationEvent.Id, integrationEvent);

            //    var dto = new EmaCrossDTO(integrationEvent.BaseAsset, integrationEvent.Interval, integrationEvent.ValueShort, integrationEvent.ValueLong, integrationEvent.PrevValueShort, integrationEvent.PrevValueLong);

            //    await _hubContext.Clients.All.SendAsync(nameof(EmaCrossDTO), dto);
            //}
        }
    }
}