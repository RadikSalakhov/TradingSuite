using Binance.Worker.IntegrationEvents;
using Binance.Domain.Common;
using Binance.Domain.Services;
using Binance.Infrastructure.Services;
using EventBus.Abstraction;
using Services.Common.WorkerHandlers;

namespace Binance.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IWorkerHandler _workerHandler;

        private readonly IEventBus _eventBus;

        private readonly IBinancePriceTickerService _binancePriceTickerService;

        private readonly ITechnicalIndicatorsService _technicalIndicatorsService;

        private readonly List<CryptoAsset> _supportedCryptoAssets = new List<CryptoAsset>();

        public Worker(ILogger<Worker> logger, IWorkerHandler workerHandler, IEventBus eventBus, IBinancePriceTickerService binancePriceTickerService, ITechnicalIndicatorsService technicalIndicatorsService)
        {
            _logger = logger;
            _workerHandler = workerHandler;
            _eventBus = eventBus;
            _binancePriceTickerService = binancePriceTickerService;
            _technicalIndicatorsService = technicalIndicatorsService;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _workerHandler.RegisterAction<Worker>(100, onEvery100);

            _workerHandler.RegisterAction<Worker>(500, onEvery500);

            _workerHandler.RegisterAction<Worker>(1000, onEvery1000);

            _workerHandler.RegisterAction<Worker>(2000, onEvery2000);

            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _workerHandler.UnregisteredActions<Worker>();

            await base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _workerHandler.ProcessStepAsync();
            }
        }

        private async Task onEvery100()
        {
            var processedCryptoAssets = _technicalIndicatorsService.ProcessPriceTickersBuffer();
            foreach (var cryptoAsset in processedCryptoAssets)
            {
                var emaCross = _technicalIndicatorsService.GetEmaCrossEntity(cryptoAsset);
                if (emaCross != null)
                {
                    var integrationEvent = new IndicatorEmaCrossIntegrationEvent(emaCross.CryptoAsset, emaCross.TAInterval, emaCross.ValueShort, emaCross.ValueLong, emaCross.PrevValueShort, emaCross.PrevValueLong);
                    _eventBus.Publish(integrationEvent);
                }
            }
        }

        private async Task onEvery500()
        {
            {//Supported CryptoAssets
                if (!_supportedCryptoAssets.Any())
                {
                    var cryptoAssets = await _binancePriceTickerService.GetSupportedCryptoAssets();
                    if (cryptoAssets.Any())
                        _supportedCryptoAssets.AddRange(cryptoAssets);
                }
            }

            {//Price Tickers
                if (_supportedCryptoAssets.Any())
                {
                    var priceTickers = await _binancePriceTickerService.GetPriceTickers(_supportedCryptoAssets);

                    _technicalIndicatorsService.AddPriceTickersToBuffer(priceTickers);

                    foreach (var priceTicker in priceTickers)
                    {
                        var integrationEvent = new BinanceCryptoAssetPriceTickerIntegrationEvent(priceTicker.CryptoAsset, priceTicker.QuoteCryptoAsset, priceTicker.Price);
                        _eventBus.Publish(integrationEvent);
                    }
                }
            }
        }

        private async Task onEvery1000()
        {
            {//ServerTime
                var serverTime = await _binancePriceTickerService.GetServerTime();
                var integrationEvent = new BinanceServerTimeIntegrationEvent(serverTime.ServerTime);

                _eventBus.Publish(integrationEvent);
            }
        }

        private Task onEvery2000()
        {
            return Task.CompletedTask;
        }
    }
}