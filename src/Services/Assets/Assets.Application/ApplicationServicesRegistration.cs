using Assets.Application.Configuration;
using Assets.Application.IntegrationEventHandlers;
using EventBus.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Common.IntegrationEvents;

namespace Assets.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AssetsApiOptions>(configuration.GetSection(nameof(AssetsApiOptions)));

            services.AddSingleton<IIntegrationEventHandler<AssetPriceTickerIntegrationEvent>, AssetPriceTickerIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<IndicatorEmaCrossIntegrationEvent>, IndicatorEmaCrossIntegrationEventHandler>();

            return services;
        }
    }
}