using EventBus.Abstraction;
using EventBus.Subscriptions;
using EventBusRabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Services.Common.Extensions
{
    public static class CommonExtensions
    {
        public static IServiceCollection AddServiceDefaults(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEventBus(configuration);

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var eventBusSection = configuration.GetSection("EventBus");
            if (!eventBusSection.Exists())
                return services;

            if (string.Equals(eventBusSection["ProviderName"], "RabbitMQ", StringComparison.OrdinalIgnoreCase))
            {
                services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
                {
                    var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                    var factory = new ConnectionFactory()
                    {
                        HostName = configuration.GetRequiredConnectionString("EventBus"),
                        DispatchConsumersAsync = true
                    };

                    if (!string.IsNullOrEmpty(eventBusSection["UserName"]))
                        factory.UserName = eventBusSection["UserName"];

                    if (!string.IsNullOrEmpty(eventBusSection["Password"]))
                        factory.Password = eventBusSection["Password"];

                    var retryCount = eventBusSection.GetValue("RetryCount", 5);

                    return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
                });

                services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
                {
                    var subscriptionClientName = eventBusSection.GetRequiredValue("SubscriptionClientName");
                    var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                    var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
                    var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                    var retryCount = eventBusSection.GetValue("RetryCount", 5);

                    return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, logger, sp, eventBusSubscriptionsManager, subscriptionClientName, retryCount);
                });
            }

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            return services;
        }
    }
}