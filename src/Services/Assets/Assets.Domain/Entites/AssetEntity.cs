using Assets.Domain.Base;
using Assets.Domain.Keys;

namespace Assets.Domain.Entites
{
    public class AssetEntity : BaseEntity<AssetKey>
    {
        public string AssetType { get; }

        public string BaseAsset { get; }

        public decimal LotStepSize { get; set; }

        public AssetEntity(string assetType, string baseAsset)
        {
            AssetType = assetType;
            BaseAsset = baseAsset;
        }

        public override AssetKey GetKey()
        {
            return new AssetKey(AssetType, BaseAsset);
        }
    }
}