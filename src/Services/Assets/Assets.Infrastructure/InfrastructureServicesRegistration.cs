using Assets.Application.Configuration;
using Assets.Application.Contracts;
using Assets.Infrastructure.BinanceWorkerApiServices;
using Assets.Infrastructure.CacheServices;
using Assets.Infrastructure.Interceptors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Assets.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<GrpcExceptionInterceptor>();

            services.AddSingleton<ICacheService, CacheService>();

            services.AddScoped<IBinanceWorkerApiService, BinanceWorkerApiService>();

            services.AddGrpcClient<BinanceWorkerAPI.BinanceWorker.BinanceWorkerClient>(
                (services, options) =>
                {
                    var url = services.GetRequiredService<IOptions<AssetsOptions>>().Value.BinanceWorkerApiGrpc;
                    options.Address = new Uri(url);
                })
                .AddInterceptor<GrpcExceptionInterceptor>();
            return services;
        }
    }
}