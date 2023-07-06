using BlazorApp.Client.Entities;
using BlazorApp.Client.Services.ClientCacheEntries;

namespace BlazorApp.Client.Abstraction
{
    public interface IClientCacheService
    {
        AssetCacheEntry Asset { get; }

        ServerTimeCacheEntry ServerTime { get; }
    }
}