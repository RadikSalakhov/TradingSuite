using BlazorApp.Client.Services.ClientCacheEntries;

namespace BlazorApp.Client.Entities
{
    public class AssetEntity
    {
        public string AssetId { get; set; }

        public AssetPriceEntity AssetPrice { get; set; }

        public AssetEntity(string assetId)
        {
            AssetId = assetId;
            AssetPrice = new AssetPriceEntity(assetId, "USDT", 0m);
        }
    }
}