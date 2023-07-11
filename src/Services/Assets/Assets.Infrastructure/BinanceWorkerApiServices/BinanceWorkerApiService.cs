using Assets.Application.Contracts;
using Assets.Domain.Entites;
using Services.Common;

namespace Assets.Infrastructure.BinanceWorkerApiServices
{
    public class BinanceWorkerApiService : IBinanceWorkerApiService
    {
        private BinanceWorkerAPI.BinanceWorker.BinanceWorkerClient _client;

        public BinanceWorkerApiService(BinanceWorkerAPI.BinanceWorker.BinanceWorkerClient client)
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
                var asset = AssetEntity.Create(binanceAsset.AssetType, binanceAsset.BaseAsset);
                asset.LotStepSize = CommonUtilities.ToDecimal(binanceAsset.LotStepSize.Units, binanceAsset.LotStepSize.Nanos);
                result.Add(asset);
            }

            return result;
        }
    }
}