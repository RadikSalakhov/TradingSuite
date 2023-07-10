using Assets.Application.Contracts.RepositoryServices.Base;
using Assets.Domain.Entites;
using Assets.Domain.Keys;

namespace Assets.Application.Contracts.RepositoryServices
{
    public interface IAssetRepositoryService : IBaseRepositoryService<AssetKey, AssetEntity>
    {
    }
}