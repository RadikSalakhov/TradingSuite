using BlazorApp.Client.Entities;
using BlazorApp.Client.Services.ClientCacheEntries.Base;

namespace BlazorApp.Client.Services.ClientCacheEntries
{
    public class EmaCrossCacheEntry : BaseDictCacheEntry<string, EmaCrossEntity>
    {
        public EmaCrossEntity? Get(string assetId, string interval)
        {
            return GetByKey($"{assetId} - {interval}");
        }

        protected override string GetKey(EmaCrossEntity value)
        {
            return value.Id;
        }
    }
}