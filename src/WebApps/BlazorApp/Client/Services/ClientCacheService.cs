using BlazorApp.Client.Abstraction;
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

        public AssetEntity GetAssetEntity(string baseAsset)
        {
            var assetEntity = Asset.GetByKey(baseAsset);
            if (assetEntity == null)
                assetEntity = new AssetEntity("CRYPTO", baseAsset);

            var assetPriceEntity = AssetPrice.GetByKeys("CRYPTO", baseAsset);
            if (assetPriceEntity == null)
                assetPriceEntity = new AssetPriceEntity("CRYPTO", baseAsset, "USDT", 0m);

            assetEntity.AssetPrice = assetPriceEntity;

            return assetEntity;
        }
    }
}