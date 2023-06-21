namespace Services.Common.Extensions
{
    public static class LinqExtensions
    {
        public static void SortBy<TSource, TKey>(this IList<TSource> source, Func<TSource, TKey> keySelector)
        {
            var sortedList = source.OrderBy(keySelector).ToList();

            lock (source)
            {
                source.Clear();
                sortedList.ForEach(v => source.Add(v));
            }
        }

        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
        {
            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                    yield return YieldBatchElements(enumerator, batchSize);
            }
        }

        private static IEnumerable<T> YieldBatchElements<T>(IEnumerator<T> source, int batchSize)
        {
            yield return source.Current;
            for (int i = 0; i < batchSize - 1 && source.MoveNext(); i++)
                yield return source.Current;
        }
    }
}