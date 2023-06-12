using Binance.Domain.Common;
using Binance.Domain.Services;

namespace Binance.BackgroundTasks
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IBinancePriceTickerService _binancePriceTickerService;

        public Worker(ILogger<Worker> logger, IBinancePriceTickerService binancePriceTickerService)
        {
            _logger = logger;
            _binancePriceTickerService = binancePriceTickerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var serverTime = await _binancePriceTickerService.GetServerTime();

                var priceTickers = await _binancePriceTickerService.GetPriceTickers(CryptoAsset.GetAll(skipUSDT: true));

                await Task.Delay(500);
            }
        }
    }
}