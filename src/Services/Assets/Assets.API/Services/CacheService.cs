using Assets.API.Abstraction;
using Assets.API.Services.CacheEntries;

namespace Assets.API.Services
{
    public class CacheService : ICacheService
    {
        public AssetCacheEntry Asset { get; }

        public CacheService()
        {
            Asset = new AssetCacheEntry();
        }
    }
}