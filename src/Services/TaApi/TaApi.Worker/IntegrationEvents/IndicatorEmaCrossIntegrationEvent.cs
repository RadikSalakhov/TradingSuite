using EventBus.Events;

namespace TaApi.Worker.IntegrationEvents
{
    public class IndicatorEmaCrossIntegrationEvent : IntegrationEvent
    {
        public string AssetId { get; }

        public string Interval { get; }

        public decimal ValueShort { get; }

        public decimal ValueLong { get; }

        public decimal PrevValueShort { get; }

        public decimal PrevValueLong { get; }

        public IndicatorEmaCrossIntegrationEvent(string assetId, string interval, decimal valueShort, decimal valueLong, decimal prevValueShort, decimal prevValueLong)
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