using BlazorApp.Client.Entities;
using BlazorApp.Client.Services.ClientCacheEntries.Base;

namespace BlazorApp.Client.Services.ClientCacheEntries
{
    public class ServerTimeCacheEntry : BaseValueCacheEntry<DateTime, ServerTimeEntity>
    {
        protected override DateTime GetKey(ServerTimeEntity value)
        {
            return value.ServerTime;
        }
    }
}