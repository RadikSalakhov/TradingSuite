using Assets.API.IntegrationEvents;
using EventBus.Abstraction;
using Microsoft.AspNetCore.SignalR;

namespace Assets.API.IntegrationEventHandlers
{
    public class AssetPriceTickerIntegrationEventHandler : IIntegrationEventHandler<AssetPriceTickerIntegrationEvent>
    {
        public AssetPriceTickerIntegrationEventHandler()
        {
        }

        public async Task Handle(AssetPriceTickerIntegrationEvent integrationEvent)
        {
            //using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new("IntegrationEventContext", integrationEvent.Id) }))
            //{
            //    _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", integrationEvent.Id, integrationEvent);

            //    var dto = new AssetPriceDTO(integrationEvent.AssetType, integrationEvent.BaseAsset, integrationEvent.QuoteAsset, integrationEvent.Price);

            //    await _hubContext.Clients.All.SendAsync(nameof(AssetPriceDTO), dto);
            //}
        }
    }
}