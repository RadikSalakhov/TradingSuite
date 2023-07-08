using Binance.Domain.Services;
using BinanceWorkerAPI;
using Grpc.Core;

namespace Binance.Worker.Grpc
{
    public class BinanceWorkerAPIService : BinanceWorkerAPI.BinanceWorker.BinanceWorkerBase
    {
        private readonly IBinancePriceTickerService _binancePriceTickerService;
        private readonly ILogger<BinanceWorkerAPIService> _logger;

        public BinanceWorkerAPIService(IBinancePriceTickerService binancePriceTickerService, ILogger<BinanceWorkerAPIService> logger)
        {
            _binancePriceTickerService = binancePriceTickerService;
            _logger = logger;
        }

        public override async Task<BinanceAssetsResponse> GetBinanceAssets(BinanceAssetsRequest request, ServerCallContext context)
        {
            try
            {
                _logger.LogInformation("START");

                var assets = await _binancePriceTickerService.GetAllSupportedCryptoAssets();

                if (assets != null)
                {
                    var assetsStr = string.Join(",", assets);
                    _logger.LogInformation($"Assets: {assetsStr}");
                }
                else
                    _logger.LogInformation("Assets: NULL");

                var result = new BinanceAssetsResponse();

                if (assets != null && assets.Any())
                    result.AssetBase.AddRange(assets.Select(v => v.ToString()));

                return result;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, exp.Message);

                throw;
            }
        }
    }
}