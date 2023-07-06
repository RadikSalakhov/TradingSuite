namespace BlazorApp.Client.Entities
{
    public class AssetEntity
    {
        public string AssetType { get; }

        public string BaseAsset { get; }

        public AssetPriceEntity AssetPrice { get; set; }

        public AssetEntity(string assetType, string baseAsset)
        {
            AssetType = assetType;
            BaseAsset = baseAsset;
            AssetPrice = new AssetPriceEntity(AssetType, BaseAsset, "USDT", 0m);
        }
    }
}