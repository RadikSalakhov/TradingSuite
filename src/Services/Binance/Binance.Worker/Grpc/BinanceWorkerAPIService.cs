using Binance.Domain.Services;
using BinanceWorkerAPI;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Services.Common;

namespace Binance.Worker.Grpc
{
    public class BinanceWorkerAPIService : BinanceWorker.BinanceWorkerBase
    {
        private readonly IBinancePriceTickerService _binancePriceTickerService;
        private readonly ILogger<BinanceWorkerAPIService> _logger;

        public BinanceWorkerAPIService(IBinancePriceTickerService binancePriceTickerService, ILogger<BinanceWorkerAPIService> logger)
        {
            _binancePriceTickerService = binancePriceTickerService;
            _logger = logger;
        }

        public override async Task<GetBinanceAssetsResponse> GetBinanceAssets(Empty request, ServerCallContext context)
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

                var result = new GetBinanceAssetsResponse();

                if (assets != null && assets.Any())
                {
                    foreach (var asset in assets)
                    {
                        var lotStepSizeTuple = CommonUtilities.FromDecimal(asset.LotStepSize);
                        var lotStepSize = new DecimalValue
                        {
                            Units = lotStepSizeTuple.Item1,
                            Nanos = lotStepSizeTuple.Item2
                        };

                        var binanceAsset = new BinanceAsset
                        {
                            AssetType = asset.AssetType,
                            BaseAsset = asset.BaseAsset,
                            LotStepSize = lotStepSize
                        };
                        result.Assets.Add(binanceAsset);
                    }
                }

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