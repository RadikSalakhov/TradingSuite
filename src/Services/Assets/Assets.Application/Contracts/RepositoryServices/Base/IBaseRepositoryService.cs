using Assets.Domain.Base;

namespace Assets.Application.Contracts.RepositoryServices.Base
{
    public interface IBaseRepositoryService<TKey, TEntity>
        where TKey : BaseKey
        where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity?> GetByKey(TKey key);

        Task<IEnumerable<TEntity>> CreateOrUpdate(IEnumerable<TEntity> entities);

        Task<TEntity?> CreateOrUpdate(TEntity entity);
    }
}