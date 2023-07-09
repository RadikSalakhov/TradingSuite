using Assets.API.Entites;

namespace Assets.API.Abstraction
{
    public interface IBinanceWorkerAPIService
    {
        Task<IEnumerable<AssetEntity>> GetBinanceAssets();
    }
}