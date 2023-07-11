namespace Assets.Domain.Base
{
    public abstract class BaseEntity<TKey>
        where TKey : BaseKey
    {
        public TKey Key { get; }

        protected BaseEntity(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            Key = key;
        }
    }
}