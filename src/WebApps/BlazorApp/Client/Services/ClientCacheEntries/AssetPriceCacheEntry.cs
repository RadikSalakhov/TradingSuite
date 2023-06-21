using BlazorApp.Client.Entities;
using BlazorApp.Client.Services.ClientCacheEntries.Base;

namespace BlazorApp.Client.Services.ClientCacheEntries
{
    public class AssetPriceCacheEntry : BaseDictCacheEntry<string, AssetPriceEntity>
    {
        protected override string GetKey(AssetPriceEntity value)
        {
            return value.AssetId;
        }
    }
}