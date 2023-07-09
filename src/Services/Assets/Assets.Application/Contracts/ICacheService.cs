using Assets.Application.Contracts.CacheEntries;

namespace Assets.Application.Contracts
{
    public interface ICacheService
    {
        AssetAggregateCacheEntry AssetAggregate { get; }
    }
}