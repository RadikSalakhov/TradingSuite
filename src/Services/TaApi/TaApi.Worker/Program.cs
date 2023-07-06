using Services.Common.Extensions;
using TaApi.Worker;
using TaApi.Worker.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddServiceDefaults(hostContext.Configuration);

        services.AddTaApiServices(hostContext.Configuration);

        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();