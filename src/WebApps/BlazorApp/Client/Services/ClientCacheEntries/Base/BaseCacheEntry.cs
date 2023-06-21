namespace BlazorApp.Client.Services.ClientCacheEntries.Base
{
    public abstract class BaseCacheEntry<TKey, TValue>
    {
        public event Func<TValue?, Task>? Updated;

        protected abstract TKey GetKey(TValue value);

        protected async Task RaiseUpdated(TValue? value)
        {
            var updatedFunc = Updated;

            if (updatedFunc != null)
                await updatedFunc.Invoke(value);
        }
    }
}