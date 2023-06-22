using BlazorApp.Client.Entities;
using BlazorApp.Client.Services.ClientCacheEntries;

namespace BlazorApp.Client.Abstraction
{
    public interface IClientCacheService
    {
        AssetCacheEntry Asset { get; }

        AssetPriceCacheEntry AssetPrice { get; }

        EmaCrossCacheEntry EmaCross { get; }

        ServerTimeCacheEntry ServerTime { get; }

        AssetEntity GetAssetEntity(string assetId);
    }
}