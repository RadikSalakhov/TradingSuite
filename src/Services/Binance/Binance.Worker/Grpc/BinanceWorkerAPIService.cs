using Binance.Domain.Services;
using BinanceWorkerAPI;
using Grpc.Core;

namespace Binance.Worker.Grpc
{
    public class BinanceWorkerAPIService : BinanceWorkerAPI.BinanceWorker.BinanceWorkerBase
    {
        private readonly IBinancePriceTickerService _binancePriceTickerService;

        public BinanceWorkerAPIService(IBinancePriceTickerService binancePriceTickerService)
        {
            _binancePriceTickerService = binancePriceTickerService;
        }

        public override async Task<BinanceAssetsResponse> GetBinanceAssets(BinanceAssetsRequest request, ServerCallContext context)
        {
            var assets = await _binancePriceTickerService.GetAllSupportedCryptoAssets();

            var result = new BinanceAssetsResponse();

            if (assets != null && assets.Any())
                result.AssetBase.AddRange(assets.Select(v => v.ToString()));

            return result;
        }
    }
}