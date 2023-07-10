using Assets.Domain.Base;

namespace Assets.Persistence.Base
{
    public abstract class BaseDB<TKey, TEntity, TDB>
        where TKey : BaseKey
        where TEntity : BaseEntity<TKey>
        where TDB : BaseDB<TKey, TEntity, TDB>
    {
        public abstract TKey GetKey();

        public abstract void FromEntity(TEntity entity);

        public abstract TEntity ToEntity();
    }
}