using BlazorApp.Client.DTO;
using BlazorApp.Server.IntegrationEvents;
using EventBus.Abstraction;
using Microsoft.AspNetCore.SignalR;

namespace BlazorApp.Server.IntegrationEventHandlers
{
    public class BinanceServerTimeIntegrationEventHandler : IIntegrationEventHandler<BinanceServerTimeIntegrationEvent>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<BinanceCryptoAssetPriceTickerIntegrationEventHandler> _logger;

        public BinanceServerTimeIntegrationEventHandler(
            IHubContext<NotificationsHub> hubContext,
            ILogger<BinanceCryptoAssetPriceTickerIntegrationEventHandler> logger)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(BinanceServerTimeIntegrationEvent integrationEvent)
        {
            using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new("IntegrationEventContext", integrationEvent.Id) }))
            {
                _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", integrationEvent.Id, integrationEvent);

                var dto = new ServerTimeDTO(integrationEvent.ServerTime);

                //await _hubContext.Clients.All.SendAsync(nameof(ServerTimeDTO), dto);
            }
        }
    }
}