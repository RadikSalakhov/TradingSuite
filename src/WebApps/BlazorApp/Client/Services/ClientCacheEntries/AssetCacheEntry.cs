using BlazorApp.Client.Entities;
using BlazorApp.Client.Services.ClientCacheEntries.Base;

namespace BlazorApp.Client.Services.ClientCacheEntries
{
    public class AssetCacheEntry : BaseDictCacheEntry<string, AssetEntity>
    {
        protected override string GetKey(AssetEntity value)
        {
            return value.BaseAsset;
        }
    }
}