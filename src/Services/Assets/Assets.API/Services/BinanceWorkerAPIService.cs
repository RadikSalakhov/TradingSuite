using Assets.Application.Contracts;
using Assets.Domain.Entites;
using Services.Common;

namespace Assets.API.Services
{
    public class BinanceWorkerAPIService : IBinanceWorkerAPIService
    {
        private BinanceWorkerAPI.BinanceWorker.BinanceWorkerClient _client;

        public BinanceWorkerAPIService(BinanceWorkerAPI.BinanceWorker.BinanceWorkerClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<AssetEntity>> GetBinanceAssets()
        {
            var response = await _client.GetBinanceAssetsAsync(new Google.Protobuf.WellKnownTypes.Empty());
            if (response == null || response.Assets == null)
                return Array.Empty<AssetEntity>();

            var result = new List<AssetEntity>();

            foreach (var binanceAsset in response.Assets)
            {
                var asset = new AssetEntity(binanceAsset.AssetType, binanceAsset.BaseAsset);
                asset.LotStepSize = CommonUtilities.ToDecimal(binanceAsset.LotStepSize.Units, binanceAsset.LotStepSize.Nanos);
                result.Add(asset);
            }

            return result;
        }
    }
}