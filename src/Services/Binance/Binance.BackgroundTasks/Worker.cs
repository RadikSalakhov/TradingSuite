using Binance.BackgroundTasks.IntegrationEvents;
using Binance.Domain.Common;
using Binance.Domain.Services;
using EventBus.Abstraction;
using Services.Common.WorkerHandlers;

namespace Binance.BackgroundTasks
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IWorkerHandler _workerHandler;

        private readonly IEventBus _eventBus;

        private readonly IBinancePriceTickerService _binancePriceTickerService;

        public Worker(ILogger<Worker> logger, IWorkerHandler workerHandler, IEventBus eventBus, IBinancePriceTickerService binancePriceTickerService)
        {
            _logger = logger;
            _workerHandler = workerHandler;
            _eventBus = eventBus;
            _binancePriceTickerService = binancePriceTickerService;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
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

        private async Task onEvery500()
        {
            {//Price Tickers
                var priceTickers = await _binancePriceTickerService.GetPriceTickers(CryptoAsset.GetAll(skipUSDT: true));

                foreach (var priceTicker in priceTickers)
                {
                    var integrationEvent = new BinanceCryptoAssetPriceTickerIntegrationEvent(priceTicker.CryptoAsset, priceTicker.BaseCryptoAsset, priceTicker.Price);
                    _eventBus.Publish(integrationEvent);
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