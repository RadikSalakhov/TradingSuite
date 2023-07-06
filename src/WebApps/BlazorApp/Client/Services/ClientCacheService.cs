using BlazorApp.Client.Abstraction;
using BlazorApp.Client.Entities;
using BlazorApp.Client.Services.ClientCacheEntries;

namespace BlazorApp.Client.Services
{
    public class ClientCacheService : IClientCacheService
    {
        public AssetCacheEntry Asset { get; } = new AssetCacheEntry();

        public ServerTimeCacheEntry ServerTime { get; } = new ServerTimeCacheEntry();
    }
}