namespace Assets.Domain.Entites
{
    public class AssetEntity
    {
        public string AssetType { get; }

        public string BaseAsset { get; }

        public decimal LotStepSize { get; set; }

        public AssetEntity(string assetType, string baseAsset)
        {
            AssetType = assetType;
            BaseAsset = baseAsset;
        }
    }
}