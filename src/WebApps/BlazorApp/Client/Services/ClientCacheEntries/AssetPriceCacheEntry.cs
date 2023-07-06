using BlazorApp.Client.Entities;
using BlazorApp.Client.Services.ClientCacheEntries.Base;

namespace BlazorApp.Client.Services.ClientCacheEntries
{
    public class AssetPriceCacheEntry : BaseDoubleDictCacheEntry<string, string, AssetPriceEntity>
    {
        protected override string GetKeyA(AssetPriceEntity value)
        {
            return value.AssetType;
        }

        protected override string GetKeyB(AssetPriceEntity value)
        {
            return value.BaseAsset;
        }
    }
}