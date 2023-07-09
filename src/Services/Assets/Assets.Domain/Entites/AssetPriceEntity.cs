namespace Assets.Domain.Entites
{
    public class AssetPriceEntity
    {
        public string AssetType { get; }

        public string BaseAsset { get; }

        public decimal PriceUSDT { get; set; }

        public AssetPriceEntity(string assetType, string baseAsset)
        {
            AssetType = assetType;
            BaseAsset = baseAsset;
        }
    }
}