using EventBus.Events;

namespace Assets.API.IntegrationEvents
{
    public class IndicatorEmaCrossIntegrationEvent : IntegrationEvent
    {
        public string BaseAsset { get; }

        public string Interval { get; }

        public decimal ValueShort { get; }

        public decimal ValueLong { get; }

        public decimal PrevValueShort { get; }

        public decimal PrevValueLong { get; }

        public IndicatorEmaCrossIntegrationEvent(string baseAsset, string interval, decimal valueShort, decimal valueLong, decimal prevValueShort, decimal prevValueLong)
        {
            BaseAsset = baseAsset;
            Interval = interval;
            ValueShort = valueShort;
            ValueLong = valueLong;
            PrevValueShort = prevValueShort;
            PrevValueLong = prevValueLong;
        }
    }
}