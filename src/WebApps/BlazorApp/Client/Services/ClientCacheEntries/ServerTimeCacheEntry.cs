using BlazorApp.Client.Entities;
using BlazorApp.Client.Services.ClientCacheEntries.Base;

namespace BlazorApp.Client.Services.ClientCacheEntries
{
    public class ServerTimeCacheEntry : BaseValueCacheEntry<ServerTimeEntity>
    {
        //protected DateTime GetKey(ServerTimeEntity value)
        //{
        //    return value.ServerTime;
        //}
    }
}