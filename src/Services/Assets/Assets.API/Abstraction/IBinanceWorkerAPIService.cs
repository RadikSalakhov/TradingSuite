namespace Assets.API.Abstraction
{
    public interface IBinanceWorkerAPIService
    {
        Task<IEnumerable<string>> GetBinanceAssets();
    }
}