using EventBus.Events;

namespace PriceTicker.SignalrHub.IntegrationEvents
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