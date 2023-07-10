namespace Assets.Domain.Base
{
    public abstract class BaseEntity<TKey>
        where TKey : BaseKey
    {
        public abstract TKey GetKey();
    }
}