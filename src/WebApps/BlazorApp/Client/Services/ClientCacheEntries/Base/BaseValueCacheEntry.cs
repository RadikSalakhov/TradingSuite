namespace BlazorApp.Client.Services.ClientCacheEntries.Base
{
    public abstract class BaseValueCacheEntry<TKey, TValue> : BaseCacheEntry<TKey, TValue>
        where TValue : class, new()
    {
        private TValue _value = new();

        public TValue Get()
        {
            return _value;
        }

        public virtual async Task UpdateAsync(TValue value)
        {
            _value = value ?? new();

            await RaiseUpdated(value);
        }
    }
}