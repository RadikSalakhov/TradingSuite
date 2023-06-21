using EventBus.Abstraction;
using Ticker.SignalrHub;
using Ticker.SignalrHub.IntegrationEventHandlers;
using Ticker.SignalrHub.IntegrationEvents;
using Services.Common.Extensions;

var corsPolicyName = "priceticker-signalrhub-cors-policy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceDefaults(builder.Configuration);

builder.Services.AddSignalR();

builder.Services.AddSingleton<IIntegrationEventHandler<BinanceCryptoAssetPriceTickerIntegrationEvent>, BinanceCryptoAssetPriceTickerIntegrationEventHandler>();
builder.Services.AddSingleton<IIntegrationEventHandler<BinanceServerTimeIntegrationEvent>, BinanceServerTimeIntegrationEventHandler>();
builder.Services.AddSingleton<IIntegrationEventHandler<TaApiEmaCrossIntegrationEvent>, TaApiEmaCrossIntegrationEventHandler>();

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

eventBus.Subscribe<BinanceCryptoAssetPriceTickerIntegrationEvent, IIntegrationEventHandler<BinanceCryptoAssetPriceTickerIntegrationEvent>>();
eventBus.Subscribe<BinanceServerTimeIntegrationEvent, IIntegrationEventHandler<BinanceServerTimeIntegrationEvent>>();
eventBus.Subscribe<TaApiEmaCrossIntegrationEvent, IIntegrationEventHandler<TaApiEmaCrossIntegrationEvent>>();

await app.RunAsync();