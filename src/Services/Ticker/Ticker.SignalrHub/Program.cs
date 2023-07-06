using EventBus.Abstraction;
using Services.Common.Extensions;
using Services.Common.IntegrationEvents;
using Ticker.SignalrHub;
using Ticker.SignalrHub.IntegrationEventHandlers;

var corsPolicyName = "priceticker-signalrhub-cors-policy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceDefaults(builder.Configuration);

builder.Services.AddSignalR();

builder.Services.AddSingleton<IIntegrationEventHandler<AssetPriceTickerIntegrationEvent>, AssetPriceTickerIntegrationEventHandler>();
builder.Services.AddSingleton<IIntegrationEventHandler<BinanceServerTimeIntegrationEvent>, BinanceServerTimeIntegrationEventHandler>();
builder.Services.AddSingleton<IIntegrationEventHandler<IndicatorEmaCrossIntegrationEvent>, IndicatorEmaCrossIntegrationEventHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName,
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors(corsPolicyName);

app.MapHub<NotificationsHub>("/hub");

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<AssetPriceTickerIntegrationEvent, IIntegrationEventHandler<AssetPriceTickerIntegrationEvent>>();
eventBus.Subscribe<BinanceServerTimeIntegrationEvent, IIntegrationEventHandler<BinanceServerTimeIntegrationEvent>>();
eventBus.Subscribe<IndicatorEmaCrossIntegrationEvent, IIntegrationEventHandler<IndicatorEmaCrossIntegrationEvent>>();

await app.RunAsync();