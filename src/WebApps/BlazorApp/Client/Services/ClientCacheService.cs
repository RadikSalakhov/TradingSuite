using BlazorApp.Client.Abstraction;
using BlazorApp.Client.Services.ClientCacheEntries;

namespace BlazorApp.Client.Services
{
    public class ClientCacheService : IClientCacheService
    {
        public AssetCacheEntry Asset { get; }

        public ServerTimeCacheEntry ServerTime { get; }

        public ClientCacheService(IServiceProvider serviceProvider)
        {
            Asset = new AssetCacheEntry(serviceProvider);

            ServerTime = new ServerTimeCacheEntry();
        }
    }
}