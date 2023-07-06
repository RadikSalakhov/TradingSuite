using TaApi.Worker.Abstraction;
using TaApi.Worker.Configuration;
using TaApi.Worker.Services;

namespace TaApi.Worker.Extensions
{
    public static class TaApiServicesRegistration
    {
        public static IServiceCollection AddTaApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEmaProcessor, EmaProcessor>();

            services.AddSingleton<ITaApiClient, TaApiClient>();

            services.AddSingleton<ITaApiService, TaApiService>();

            services.Configure<TaApiOptions>(configuration.GetSection(nameof(TaApiOptions)));

            return services;
        }
    }
}