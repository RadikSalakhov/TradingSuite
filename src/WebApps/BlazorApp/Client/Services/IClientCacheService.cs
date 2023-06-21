using BlazorApp.Client.Entities;
using BlazorApp.Client.Services.ClientCacheEntries;

namespace BlazorApp.Client.Services
{
    public interface IClientCacheService
    {
        AssetCacheEntry Asset { get; }

        AssetPriceCacheEntry AssetPrice { get; }

        ServerTimeCacheEntry ServerTime { get; }

        AssetEntity GetAssetEntity(string assetId);
    }
}