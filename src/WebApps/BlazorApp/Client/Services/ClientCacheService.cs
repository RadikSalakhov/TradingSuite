using BlazorApp.Client.Entities;
using BlazorApp.Client.Services.ClientCacheEntries;

namespace BlazorApp.Client.Services
{
    public class ClientCacheService : IClientCacheService
    {
        public AssetCacheEntry Asset { get; } = new AssetCacheEntry();

        public AssetPriceCacheEntry AssetPrice { get; } = new AssetPriceCacheEntry();

        public EmaCrossCacheEntry EmaCross { get; } = new EmaCrossCacheEntry();

        public ServerTimeCacheEntry ServerTime { get; } = new ServerTimeCacheEntry();

        public AssetEntity GetAssetEntity(string assetId)
        {
            var assetEntity = Asset.GetByKey(assetId);
            if (assetEntity == null)
                assetEntity = new AssetEntity(assetId);

            var assetPriceEntity = AssetPrice.GetByKey(assetId);
            if (assetPriceEntity == null)
                assetPriceEntity = new AssetPriceEntity(assetId, "USDT", 0m);

            assetEntity.AssetPrice = assetPriceEntity;

            return assetEntity;
        }
    }
}