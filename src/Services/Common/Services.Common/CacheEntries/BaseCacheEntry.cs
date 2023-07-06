namespace Services.Common.CacheEntries
{
    public abstract class BaseCacheEntry<TValue>
    {
        public event Func<TValue?, Task>? Updated;

        protected async Task RaiseUpdated(TValue? value)
        {
            var updatedFunc = Updated;

            if (updatedFunc != null)
                await updatedFunc.Invoke(value);
        }
    }
}