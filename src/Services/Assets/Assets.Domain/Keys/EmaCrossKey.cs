using Assets.Domain.Base;

namespace Assets.Domain.Keys
{
    public class EmaCrossKey : BaseKey
    {
        public string AssetType { get; private set; }

        public string BaseAsset { get; private set; }

        public string Interval { get; private set; }

        public EmaCrossKey(string assetType, string baseAsset, string interval)
        {
            AssetType = assetType;
            BaseAsset = baseAsset;
            Interval = interval;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(AssetType)
                && !string.IsNullOrWhiteSpace(BaseAsset)
                && !string.IsNullOrWhiteSpace(Interval);
        }
    }
}