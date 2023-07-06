using EventBus.Events;

namespace Binance.Worker.IntegrationEvents
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