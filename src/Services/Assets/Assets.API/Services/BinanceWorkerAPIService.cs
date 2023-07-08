using Assets.API.Abstraction;

namespace Assets.API.Services
{
    public class BinanceWorkerAPIService : IBinanceWorkerAPIService
    {
        private BinanceWorkerAPI.BinanceWorker.BinanceWorkerClient _client;

        public BinanceWorkerAPIService(BinanceWorkerAPI.BinanceWorker.BinanceWorkerClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<string>> GetBinanceAssets()
        {
            var response = await _client.GetBinanceAssetsAsync(new BinanceWorkerAPI.BinanceAssetsRequest());

            return response.AssetBase;
        }
    }
}