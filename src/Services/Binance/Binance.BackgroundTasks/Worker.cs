using Binance.BackgroundTasks.IntegrationEvents;
using Binance.Domain.Common;
using Binance.Domain.Services;
using EventBus.Abstraction;

namespace Binance.BackgroundTasks
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IEventBus _eventBus;

        private readonly IBinancePriceTickerService _binancePriceTickerService;

        public Worker(ILogger<Worker> logger, IEventBus eventBus, IBinancePriceTickerService binancePriceTickerService)
        {
            _logger = logger;
            _eventBus = eventBus;
            _binancePriceTickerService = binancePriceTickerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var serverTime = await _binancePriceTickerService.GetServerTime();

                    var priceTickers = await _binancePriceTickerService.GetPriceTickers(CryptoAsset.GetAll(skipUSDT: true));

                    foreach (var priceTicker in priceTickers)
                    {
                        var integrationEvent = new BinanceCryptoAssetPriceTickerIntegrationEvent(priceTicker.CryptoAsset, priceTicker.BaseCryptoAsset, priceTicker.Price);
                        _eventBus.Publish(integrationEvent);
                    }

                    await Task.Delay(500);
                }
                catch (Exception exp)
                {
                    _logger.LogError(exp, string.Empty);
                }
            }
        }
    }
}