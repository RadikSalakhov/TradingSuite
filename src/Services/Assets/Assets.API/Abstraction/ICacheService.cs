using Assets.API.Services.CacheEntries;

namespace Assets.API.Abstraction
{
    public interface ICacheService
    {
        AssetCacheEntry Asset { get; }
    }
}