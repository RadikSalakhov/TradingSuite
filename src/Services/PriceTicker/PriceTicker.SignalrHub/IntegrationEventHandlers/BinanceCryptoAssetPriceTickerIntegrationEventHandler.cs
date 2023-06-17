using EventBus.Abstraction;
using Microsoft.AspNetCore.SignalR;
using PriceTicker.SignalrHub.IntegrationEvents;

namespace PriceTicker.SignalrHub.IntegrationEventHandlers
{
    public class BinanceCryptoAssetPriceTickerIntegrationEventHandler : IIntegrationEventHandler<BinanceCryptoAssetPriceTickerIntegrationEvent>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<BinanceCryptoAssetPriceTickerIntegrationEventHandler> _logger;

        public BinanceCryptoAssetPriceTickerIntegrationEventHandler(
            IHubContext<NotificationsHub> hubContext,
            ILogger<BinanceCryptoAssetPriceTickerIntegrationEventHandler> logger)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(BinanceCryptoAssetPriceTickerIntegrationEvent integrationEvent)
        {
            using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new("IntegrationEventContext", integrationEvent.Id) }))
            {
                _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", integrationEvent.Id, integrationEvent);

                await _hubContext.Clients.All.SendAsync("PriceTicker", integrationEvent);
            }
        }
    }
}