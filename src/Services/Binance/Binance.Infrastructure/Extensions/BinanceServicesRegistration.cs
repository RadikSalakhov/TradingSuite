using Binance.Domain.Services;
using Binance.Infrastructure.Configuration;
using Binance.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Binance.Infrastructure.Extensions
{
    public static class BinanceServicesRegistration
    {
        public static IServiceCollection AddBinanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IBinancePriceTickerService, BinancePriceTickerService>();

            services.AddSingleton<ITechnicalIndicatorsService, TechnicalIndicatorsService>();

            services.Configure<BinanceOptions>(configuration.GetSection(nameof(BinanceOptions)));

            return services;
        }
    }
}