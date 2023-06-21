using EventBus.Events;

namespace Ticker.SignalrHub.DTO
{
    public class EmaCrossDTO
    {
        public string AssetId { get; }

        public string Interval { get; }

        public decimal ValueShort { get; }

        public decimal ValueLong { get; }

        public decimal PrevValueShort { get; }

        public decimal PrevValueLong { get; }

        public EmaCrossDTO(string assetId, string interval, decimal valueShort, decimal valueLong, decimal prevValueShort, decimal prevValueLong)
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