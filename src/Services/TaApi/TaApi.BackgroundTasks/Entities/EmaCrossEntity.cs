using TaApi.BackgroundTasks.Data;

namespace TaApi.BackgroundTasks.Entities
{
    public class EmaCrossEntity : BaseEntity
    {
        public Asset Asset { get; set; }

        public TAInterval TAInterval { get; set; }

        public decimal ValueShort { get; set; }

        public decimal ValueLong { get; set; }

        public decimal PrevValueShort { get; set; }

        public decimal PrevValueLong { get; set; }

        //public decimal GetValueDiff()
        //{
        //    return ValueShort - ValueLong;
        //}

        //public decimal GetPrevValueDiff()
        //{
        //    return PrevValueShort - PrevValueLong;
        //}

        //public decimal GetPriceShortDiffCoeff(decimal price)
        //{
        //    return price > 0 ? (price - ValueShort) / price : 0m;
        //}

        //public decimal GetPriceLongDiffCoeff(decimal price)
        //{
        //    return price > 0 ? (price - ValueLong) / price : 0m;
        //}

        //public decimal GetCrossDiffCoeff(decimal price)
        //{
        //    return price > 0 ? GetValueDiff() / price : 0m;
        //}

        //public decimal GetSlopeShortCoeff()
        //{
        //    if (ValueShort == 0m || PrevValueShort == 0m)
        //        return 0m;

        //    var rawCoeff = ValueShort / PrevValueShort;

        //    return rawCoeff - 1m;
        //}

        //public decimal GetSlopeLongCoeff()
        //{
        //    if (ValueLong == 0m || PrevValueLong == 0m)
        //        return 0m;

        //    var rawCoeff = ValueLong / PrevValueLong;

        //    return rawCoeff - 1m;
        //}
    }
}