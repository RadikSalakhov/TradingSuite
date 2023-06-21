using EventBus.Abstraction;
using Microsoft.AspNetCore.SignalR;
using Ticker.SignalrHub.DTO;
using Ticker.SignalrHub.IntegrationEvents;

namespace Ticker.SignalrHub.IntegrationEventHandlers
{
    public class TaApiEmaCrossIntegrationEventHandler : IIntegrationEventHandler<TaApiEmaCrossIntegrationEvent>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<TaApiEmaCrossIntegrationEventHandler> _logger;

        public TaApiEmaCrossIntegrationEventHandler(
            IHubContext<NotificationsHub> hubContext,
            ILogger<TaApiEmaCrossIntegrationEventHandler> logger)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(TaApiEmaCrossIntegrationEvent integrationEvent)
        {
            using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new("IntegrationEventContext", integrationEvent.Id) }))
            {
                _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", integrationEvent.Id, integrationEvent);

                var dto = new EmaCrossDTO(integrationEvent.AssetId, integrationEvent.Interval, integrationEvent.ValueShort, integrationEvent.ValueLong, integrationEvent.PrevValueShort, integrationEvent.PrevValueLong);

                await _hubContext.Clients.All.SendAsync(nameof(EmaCrossDTO), dto);
            }
        }
    }
}