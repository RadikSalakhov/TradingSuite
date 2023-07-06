using Assets.API.Entites;
using Services.Common.CacheEntries;

namespace Assets.API.Services.CacheEntries
{
    public class AssetCacheEntry : BaseDoubleDictCacheEntry<string, string, AssetEntity>
    {
        protected override string GetKeyA(AssetEntity value)
        {
            return value.AssetType;
        }

        protected override string GetKeyB(AssetEntity value)
        {
            return value.BaseAsset;
        }
    }
}