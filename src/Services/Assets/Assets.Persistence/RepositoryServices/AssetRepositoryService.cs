using Assets.Application.Contracts.RepositoryServices;
using Assets.Domain.Entites;
using Assets.Domain.Keys;
using Assets.Persistence.Base;
using Assets.Persistence.Contexts;
using Assets.Persistence.DBModels;

namespace Assets.Persistence.RepositoryServices
{
    public class AssetRepositoryService : BaseRepositoryService<AssetKey, AssetEntity, AssetDB>, IAssetRepositoryService
    {
        public AssetRepositoryService(DataContext dataContext)
            : base(dataContext, dataContext.Assets)
        {
        }

        protected override IQueryable<AssetDB> ApplyFilterByKey(IQueryable<AssetDB> query, AssetKey key)
        {
            return query.Where(v => v.AssetType == key.AssetType && v.BaseAsset == key.BaseAsset);
        }
    }
}