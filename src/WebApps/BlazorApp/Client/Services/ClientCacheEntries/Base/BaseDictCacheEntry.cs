using System.Collections.Generic;

namespace BlazorApp.Client.Services.ClientCacheEntries.Base
{
    public abstract class BaseDictCacheEntry<TKey, TValue> : BaseCacheEntry<TKey, TValue>
        where TKey : notnull
    {
        private readonly Dictionary<TKey, TValue> _dict = new();

        public int GetCount()
        {
            lock (_dict)
            {
                return _dict.Count;
            }
        }

        public TValue? GetByKey(TKey key)
        {
            lock (_dict)
            {
                return _dict.ContainsKey(key)
                    ? _dict[key]
                    : default;
            }
        }

        public Dictionary<TKey, TValue> GetDict()
        {
            var result = new Dictionary<TKey, TValue>();

            lock (_dict)
            {
                foreach (var kvp in _dict)
                    result.Add(kvp.Key, kvp.Value);
            }

            return result;
        }

        public List<TValue> GetList()
        {
            var result = new List<TValue>();

            lock (_dict)
            {
                foreach (var kvp in _dict)
                    result.Add(kvp.Value);
            }

            return result;
        }

        public virtual async Task ReplaceAllAsync(IEnumerable<TValue> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            lock (_dict)
            {
                _dict.Clear();

                foreach (var value in list)
                {
                    if (value != null)
                        _dict[GetKey(value)] = value;
                }
            }

            await RaiseUpdated(default);
        }

        public virtual async Task UpdateAsync(TValue value)
        {
            TKey? key = default;

            lock (_dict)
            {
                if (value != null)
                {
                    key = GetKey(value);
                    _dict[key] = value;
                }
            }

            await RaiseUpdated(key);
        }

        public virtual async Task UpdateAsync(IEnumerable<TValue> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            lock (_dict)
            {
                foreach (var value in list)
                {
                    if (value != null)
                        _dict[GetKey(value)] = value;
                }
            }

            await RaiseUpdated(default);
        }
    }
}