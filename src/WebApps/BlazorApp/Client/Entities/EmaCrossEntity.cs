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