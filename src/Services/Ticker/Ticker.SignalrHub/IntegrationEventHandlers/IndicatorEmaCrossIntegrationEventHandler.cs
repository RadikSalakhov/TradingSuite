using EventBus.Abstraction;
using Microsoft.AspNetCore.SignalR;
using Services.Common.IntegrationEvents;
using Ticker.SignalrHub.DTO;

namespace Ticker.SignalrHub.IntegrationEventHandlers
{
    public class IndicatorEmaCrossIntegrationEventHandler : IIntegrationEventHandler<IndicatorEmaCrossIntegrationEvent>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<IndicatorEmaCrossIntegrationEventHandler> _logger;

        public IndicatorEmaCrossIntegrationEventHandler(
            IHubContext<NotificationsHub> hubContext,
            ILogger<IndicatorEmaCrossIntegrationEventHandler> logger)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(IndicatorEmaCrossIntegrationEvent integrationEvent)
        {
            using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new("IntegrationEventContext", integrationEvent.Id) }))
            {
                _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", integrationEvent.Id, integrationEvent);

                var dto = new EmaCrossDTO(
                    integrationEvent.AssetType, integrationEvent.BaseAsset,
                    integrationEvent.Interval, integrationEvent.ValueShort, integrationEvent.ValueLong, integrationEvent.PrevValueShort, integrationEvent.PrevValueLong);

                await _hubContext.Clients.All.SendAsync(nameof(EmaCrossDTO), dto);
            }
        }
    }
}