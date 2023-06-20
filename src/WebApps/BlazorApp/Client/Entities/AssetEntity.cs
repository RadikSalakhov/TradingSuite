using BlazorApp.Client.Services.ClientCacheEntries;

namespace BlazorApp.Client.Entities
{
    public class AssetEntity
    {
        public string AssetName { get; set; }

        public AssetPriceEntity AssetPrice { get; set; }

        public AssetEntity(string assetName)
        {
            AssetName = assetName;
            AssetPrice = new AssetPriceEntity(assetName, "USDT", 0m);
        }
    }
}