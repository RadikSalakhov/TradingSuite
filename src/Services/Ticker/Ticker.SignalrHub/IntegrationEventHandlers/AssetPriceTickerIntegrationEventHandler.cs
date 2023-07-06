using EventBus.Abstraction;
using Microsoft.AspNetCore.SignalR;
using Ticker.SignalrHub.DTO;
using Ticker.SignalrHub.IntegrationEvents;

namespace Ticker.SignalrHub.IntegrationEventHandlers
{
    public class AssetPriceTickerIntegrationEventHandler : IIntegrationEventHandler<AssetPriceTickerIntegrationEvent>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<AssetPriceTickerIntegrationEventHandler> _logger;

        public AssetPriceTickerIntegrationEventHandler(
            IHubContext<NotificationsHub> hubContext,
            ILogger<AssetPriceTickerIntegrationEventHandler> logger)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(AssetPriceTickerIntegrationEvent integrationEvent)
        {
            using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new("IntegrationEventContext", integrationEvent.Id) }))
            {
                _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", integrationEvent.Id, integrationEvent);

                var dto = new AssetPriceDTO(integrationEvent.AssetType, integrationEvent.BaseAsset, integrationEvent.QuoteAsset, integrationEvent.Price);

                await _hubContext.Clients.All.SendAsync(nameof(AssetPriceDTO), dto);
            }
        }
    }
}