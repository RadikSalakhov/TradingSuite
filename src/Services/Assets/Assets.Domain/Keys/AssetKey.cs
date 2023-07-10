using Assets.Domain.Base;

namespace Assets.Domain.Keys
{
    public class AssetKey : BaseKey
    {
        public string AssetType { get; private set; } = string.Empty;

        public string BaseAsset { get; private set; } = string.Empty;

        public AssetKey(string assetType, string baseAsset)
        {
            AssetType = assetType;
            BaseAsset = baseAsset;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(AssetType) && !string.IsNullOrWhiteSpace(BaseAsset);
        }
    }
}