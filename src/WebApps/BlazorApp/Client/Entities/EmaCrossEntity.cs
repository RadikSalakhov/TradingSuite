namespace BlazorApp.Client.Entities
{
    public class EmaCrossEntity
    {
        public string Id { get; }

        public string AssetId { get; }

        public string Interval { get; }

        public decimal ValueShort { get; }

        public decimal ValueLong { get; }

        public decimal PrevValueShort { get; }

        public decimal PrevValueLong { get; }

        public EmaCrossEntity(string assetId, string interval, decimal valueShort, decimal valueLong, decimal prevValueShort, decimal prevValueLong)
        {
            AssetId = assetId;
            Interval = interval;
            ValueShort = valueShort;
            ValueLong = valueLong;
            PrevValueShort = prevValueShort;
            PrevValueLong = prevValueLong;

            Id = $"{AssetId} - {Interval}";
        }
    }
}