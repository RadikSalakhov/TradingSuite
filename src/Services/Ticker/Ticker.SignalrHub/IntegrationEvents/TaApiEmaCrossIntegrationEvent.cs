using EventBus.Events;

namespace Ticker.SignalrHub.IntegrationEvents
{
    public class TaApiEmaCrossIntegrationEvent : IntegrationEvent
    {
        public string AssetId { get; }

        public string Interval { get; }

        public decimal ValueShort { get; }

        public decimal ValueLong { get; }

        public decimal PrevValueShort { get; }

        public decimal PrevValueLong { get; }

        public TaApiEmaCrossIntegrationEvent(string assetId, string interval, decimal valueShort, decimal valueLong, decimal prevValueShort, decimal prevValueLong)
        {
            AssetId = assetId;
            Interval = interval;
            ValueShort = valueShort;
            ValueLong = valueLong;
            PrevValueShort = prevValueShort;
            PrevValueLong = prevValueLong;
        }
    }
}