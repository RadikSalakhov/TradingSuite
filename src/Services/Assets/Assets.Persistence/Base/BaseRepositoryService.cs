using Assets.Application.Contracts.RepositoryServices.Base;
using Assets.Domain.Base;
using Assets.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Assets.Persistence.Base
{
    public abstract class BaseRepositoryService<TKey, TEntity, TDB> : IBaseRepositoryService<TKey, TEntity>
        where TKey : BaseKey
        where TEntity : BaseEntity<TKey>
        where TDB : BaseDB<TKey, TEntity, TDB>, new()
    {
        protected DataContext Context { get; }

        protected DbSet<TDB> DbSet { get; }

        public BaseRepositoryService(DataContext context, DbSet<TDB> dbSet)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
        }

        protected abstract IQueryable<TDB> ApplyFilterByKey(IQueryable<TDB> query, TKey key);

        protected virtual IQueryable<TDB> GetDefaultQuery()
        {
            return DbSet.AsQueryable();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var query = GetDefaultQuery();
            var records = await query
                .ToListAsync();

            return records.Select(v => v.ToEntity()).ToList();
        }

        public async Task<TEntity?> GetByKey(TKey key)
        {
            var query = GetDefaultQuery();
            query = ApplyFilterByKey(query, key);

            var record = await query.FirstOrDefaultAsync();

            return record?.ToEntity();
        }

        public async Task<IEnumerable<TEntity>> CreateOrUpdate(IEnumerable<TEntity> entities)
        {
            var resultEntities = new List<TEntity>();

            foreach (var entity in entities)
            {
                var resultEntity = await CreateOrUpdate(entity);
                if (resultEntity != null)
                    resultEntities.Add(resultEntity);
            }

            return resultEntities;
        }

        public async Task<TEntity?> CreateOrUpdate(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var key = entity.GetKey();

            if (key == null || !key.IsValid())
                throw new ArgumentException("Key is not valid");

            var query = DbSet.AsQueryable();
            query = ApplyFilterByKey(query, key);

            var record = await query.FirstOrDefaultAsync();
            if (record == null)
            {
                record = new TDB();
                record.FromEntity(entity);
                DbSet.Add(record);
            }
            else
            {
                record.FromEntity(entity);
            }

            Context.SaveChanges();

            return await GetByKey(key);
        }
    }
}