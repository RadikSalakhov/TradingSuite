using BlazorApp.Client.Entities;
using BlazorApp.Client.Services.ClientCacheEntries.Base;

namespace BlazorApp.Client.Services.ClientCacheEntries
{
    public class EmaCrossCacheEntry : BaseDoubleDictCacheEntry<string, string, EmaCrossEntity>
    {
        protected override string GetKeyA(EmaCrossEntity value)
        {
            return value.BaseAsset;
        }

        protected override string GetKeyB(EmaCrossEntity value)
        {
            return value.Interval;
        }
    }
}