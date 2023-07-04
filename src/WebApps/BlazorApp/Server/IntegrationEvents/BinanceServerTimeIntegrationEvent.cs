using EventBus.Events;

namespace BlazorApp.Server.IntegrationEvents
{
    public class BinanceServerTimeIntegrationEvent : IntegrationEvent
    {
        public DateTime ServerTime { get; }

        public BinanceServerTimeIntegrationEvent(DateTime serverTime)
        {
            ServerTime = serverTime;
        }
    }
}