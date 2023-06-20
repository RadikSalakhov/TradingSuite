namespace BlazorApp.Client.Services.ClientCacheEntries.Base
{
    public abstract class BaseCacheEntry<TKey, TValue>
    {
        public event Func<TKey?, Task>? Updated;

        protected abstract TKey GetKey(TValue value);

        protected async Task RaiseUpdated(TKey? key)
        {
            var updatedFunc = Updated;

            if (updatedFunc != null)
                await updatedFunc.Invoke(key);
        }
    }
}