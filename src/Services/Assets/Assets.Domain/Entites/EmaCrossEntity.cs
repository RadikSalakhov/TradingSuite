using Assets.Domain.Base;
using Assets.Domain.Keys;

namespace Assets.Domain.Entites
{
    public class EmaCrossEntity : BaseEntity<EmaCrossKey>
    {
        public string AssetType => Key.AssetType;

        public string BaseAsset => Key.BaseAsset;

        public string Interval => Key.Interval;

        public decimal ValueShort { get; set; }

        public decimal ValueLong { get; set; }

        public decimal PrevValueShort { get; set; }

        public decimal PrevValueLong { get; set; }

        public EmaCrossEntity(EmaCrossKey key)
            : base(key)
        {
        }

        public static EmaCrossEntity Create(string assetType, string baseAsset, string interval)
        {
            return new EmaCrossEntity(new EmaCrossKey(assetType, baseAsset, interval));
        }
    }
}