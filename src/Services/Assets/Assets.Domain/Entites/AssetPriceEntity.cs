using Assets.Domain.Base;
using Assets.Domain.Keys;

namespace Assets.Domain.Entites
{
    public class AssetPriceEntity : BaseEntity<AssetPriceKey>
    {
        public string AssetType => Key.AssetType;

        public string BaseAsset => Key.BaseAsset;

        public decimal PriceUSDT { get; set; }

        public AssetPriceEntity(AssetPriceKey key)
            : base(key)
        {
        }

        public static AssetPriceEntity Create(string assetType, string baseAsset)
        {
            return new AssetPriceEntity(new AssetPriceKey(assetType, baseAsset));
        }
    }
}