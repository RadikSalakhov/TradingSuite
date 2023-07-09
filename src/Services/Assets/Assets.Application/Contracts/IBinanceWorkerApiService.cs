using Assets.Domain.Entites;

namespace Assets.Application.Contracts
{
    public interface IBinanceWorkerApiService
    {
        Task<IEnumerable<AssetEntity>> GetBinanceAssets();
    }
}