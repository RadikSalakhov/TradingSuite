using Assets.Application.Contracts;
using Assets.Application.Contracts.CacheEntries;

namespace Assets.API.Services
{
    public class CacheService : ICacheService
    {
        public AssetAggregateCacheEntry AssetAggregate { get; }

        public CacheService()
        {
            AssetAggregate = new AssetAggregateCacheEntry();
        }
    }
}