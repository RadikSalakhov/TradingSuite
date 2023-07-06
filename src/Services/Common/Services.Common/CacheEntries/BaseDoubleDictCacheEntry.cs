namespace Services.Common.CacheEntries
{
    public abstract class BaseDoubleDictCacheEntry<TKeyA, TKeyB, TValue> : BaseCacheEntry<TValue>
        where TKeyA : notnull
        where TKeyB : notnull
    {
        private readonly Dictionary<TKeyA, Dictionary<TKeyB, TValue>> _dict = new();

        protected abstract TKeyA GetKeyA(TValue value);

        protected abstract TKeyB GetKeyB(TValue value);

        public TValue? GetByKeys(TKeyA keyA, TKeyB keyB)
        {
            lock (_dict)
            {
                if (!_dict.TryGetValue(keyA, out Dictionary<TKeyB, TValue>? dictB))
                    return default;

                return dictB.ContainsKey(keyB)
                    ? dictB[keyB]
                    : default;
            }
        }

        public List<TValue> GetList()
        {
            var result = new List<TValue>();

            lock (_dict)
            {
                foreach (var kvpA in _dict)
                    foreach (var kvpB in kvpA.Value)
                        result.Add(kvpB.Value);
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
                    updateDict(value);
            }

            await RaiseUpdated(default);
        }

        public virtual async Task UpdateAsync(TValue value)
        {
            lock (_dict)
            {
                updateDict(value);
            }

            await RaiseUpdated(value);
        }

        public virtual async Task UpdateAsync(IEnumerable<TValue> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            lock (_dict)
            {
                foreach (var value in list)
                    updateDict(value);
            }

            await RaiseUpdated(default);
        }

        private void updateDict(TValue value)
        {
            if (value == null)
                return;

            var keyA = GetKeyA(value);
            if (!_dict.TryGetValue(keyA, out Dictionary<TKeyB, TValue>? dictB))
            {
                dictB = new Dictionary<TKeyB, TValue>();
                _dict.Add(keyA, dictB);
            }

            var keyB = GetKeyB(value);
            dictB[keyB] = value;
        }
    }
}