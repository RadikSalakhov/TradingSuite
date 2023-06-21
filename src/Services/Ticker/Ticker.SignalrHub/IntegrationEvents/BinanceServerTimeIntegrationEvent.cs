using EventBus.Events;

namespace Ticker.SignalrHub.IntegrationEvents
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