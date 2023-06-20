using BlazorApp.Client.Entities;
using BlazorApp.Client.Services.ClientCacheEntries;

namespace BlazorApp.Client.Services
{
    public class ClientCacheService : IClientCacheService
    {
        public AssetCacheEntry Asset { get; } = new AssetCacheEntry();

        public AssetPriceCacheEntry AssetPrice { get; } = new AssetPriceCacheEntry();

        public ServerTimeCacheEntry ServerTime { get; } = new ServerTimeCacheEntry();

        public AssetEntity GetAssetEntity(string assetName)
        {
            var assetEntity = Asset.GetByKey(assetName);
            if (assetEntity == null)
                assetEntity = new AssetEntity(assetName);

            var assetPriceEntity = AssetPrice.GetByKey(assetName);
            if (assetPriceEntity == null)
                assetPriceEntity = new AssetPriceEntity(assetName, "USDT", 0m);

            assetEntity.AssetPrice = assetPriceEntity;

            return assetEntity;
        }
    }
}