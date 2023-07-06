namespace Assets.API.Entites
{
    public class EmaCrossEntity
    {
        public string AssetType { get; }

        public string BaseAsset { get; }

        public string Interval { get; }

        public decimal ValueShort { get; set; }

        public decimal ValueLong { get; set; }

        public decimal PrevValueShort { get; set; }

        public decimal PrevValueLong { get; set; }

        public EmaCrossEntity(string assetType, string baseAsset, string interval)
            : this(assetType, baseAsset, interval, 0m, 0m, 0m, 0m)
        {
        }

        public EmaCrossEntity(string assetType, string baseAsset, string interval, decimal valueShort, decimal valueLong, decimal prevValueShort, decimal prevValueLong)
        {
            AssetType = assetType;
            BaseAsset = baseAsset;
            Interval = interval;
            ValueShort = valueShort;
            ValueLong = valueLong;
            PrevValueShort = prevValueShort;
            PrevValueLong = prevValueLong;
        }
    }
}