using Assets.Application.Contracts.RepositoryServices;
using Assets.Persistence.RepositoryServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Assets.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAssetRepositoryService, AssetRepositoryService>();

            return services;
        }
    }
}