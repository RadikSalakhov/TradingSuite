using Assets.Application.Aggregates;
using Services.Common.CacheEntries;

namespace Assets.Application.Contracts.CacheEntries
{
    public class AssetAggregateCacheEntry : BaseDoubleDictCacheEntry<string, string, AssetAggregate>
    {
        protected override string GetKeyA(AssetAggregate value)
        {
            return value.AssetEntity.AssetType;
        }

        protected override string GetKeyB(AssetAggregate value)
        {
            return value.AssetEntity.BaseAsset;
        }
    }
}