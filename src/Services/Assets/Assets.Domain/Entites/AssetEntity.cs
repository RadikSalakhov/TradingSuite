using Assets.Domain.Base;
using Assets.Domain.Keys;

namespace Assets.Domain.Entites
{
    public class AssetEntity : BaseEntity<AssetKey>
    {
        public string AssetType => Key.AssetType;

        public string BaseAsset => Key.BaseAsset;

        public decimal LotStepSize { get; set; }

        public AssetEntity(AssetKey key)
            : base(key)
        {
        }

        public static AssetEntity Create(string assetType, string baseAsset)
        {
            return new AssetEntity(new AssetKey(assetType, baseAsset));
        }
    }
}