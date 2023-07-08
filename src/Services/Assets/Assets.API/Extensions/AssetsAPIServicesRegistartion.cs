using Assets.API.Abstraction;
using Assets.API.Configuration;
using Assets.API.Infrastructure;
using Assets.API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Assets.API.Extensions
{
    public static class AssetsAPIServicesRegistartion
    {
        public static IServiceCollection AddGrpcServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<GrpcExceptionInterceptor>();

            services.Configure<AssetsAPIOptions>(configuration.GetSection(nameof(AssetsAPIOptions)));

            services.AddScoped<IBinanceWorkerAPIService, BinanceWorkerAPIService>();

            services.AddGrpcClient<BinanceWorkerAPI.BinanceWorker.BinanceWorkerClient>(
                (services, options) =>
                {
                    var url = services.GetRequiredService<IOptions<AssetsAPIOptions>>().Value.BinanceWorkerApiGrpc;
                    options.Address = new Uri(url);
                })
                .AddInterceptor<GrpcExceptionInterceptor>();

            return services;
        }
    }
}