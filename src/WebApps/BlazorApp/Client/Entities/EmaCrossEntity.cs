namespace BlazorApp.Client.Entities
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

        public decimal GetValueDiff()
        {
            return ValueShort - ValueLong;
        }

        public decimal GetPrevValueDiff()
        {
            return PrevValueShort - PrevValueLong;
        }

        public decimal GetPriceShortDiffCoeff(decimal price)
        {
            return price > 0 ? (price - ValueShort) / price : 0m;
        }

        public decimal GetPriceLongDiffCoeff(decimal price)
        {
            return price > 0 ? (price - ValueLong) / price : 0m;
        }

        public decimal GetCrossDiffCoeff(decimal price)
        {
            return price > 0 ? GetValueDiff() / price : 0m;
        }

        public decimal GetSlopeShortCoeff()
        {
            if (ValueShort == 0m || PrevValueShort == 0m)
                return 0m;

            var rawCoeff = ValueShort / PrevValueShort;

            return rawCoeff - 1m;
        }

        public decimal GetSlopeLongCoeff()
        {
            if (ValueLong == 0m || PrevValueLong == 0m)
                return 0m;

            var rawCoeff = ValueLong / PrevValueLong;

            return rawCoeff - 1m;
        }
    }
}