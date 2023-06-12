using Binance.BackgroundTasks;
using Binance.Infrastructure.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddBinanceServices(hostContext.Configuration);
    })
    .Build();

host.Run();