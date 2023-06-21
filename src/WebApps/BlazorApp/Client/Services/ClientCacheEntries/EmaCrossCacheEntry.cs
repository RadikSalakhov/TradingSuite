using BlazorApp.Client.Entities;
using BlazorApp.Client.Services.ClientCacheEntries.Base;

namespace BlazorApp.Client.Services.ClientCacheEntries
{
    public class EmaCrossCacheEntry : BaseDictCacheEntry<string, EmaCrossEntity>
    {
        protected override string GetKey(EmaCrossEntity value)
        {
            return value.Id;
        }
    }
}