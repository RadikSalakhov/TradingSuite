using Binance.Worker;
using Binance.Infrastructure.Extensions;
using Services.Common.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddServiceDefaults(hostContext.Configuration);

        services.AddBinanceServices(hostContext.Configuration);

        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();