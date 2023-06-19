using EventBus.Events;

namespace Binance.BackgroundTasks.IntegrationEvents
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