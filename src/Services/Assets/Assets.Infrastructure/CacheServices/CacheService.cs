using Assets.Application.Contracts;
using Assets.Application.Contracts.CacheEntries;

namespace Assets.Infrastructure.CacheServices
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