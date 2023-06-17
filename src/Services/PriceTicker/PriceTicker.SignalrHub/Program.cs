using EventBus.Abstraction;
using PriceTicker.SignalrHub;
using PriceTicker.SignalrHub.IntegrationEventHandlers;
using PriceTicker.SignalrHub.IntegrationEvents;
using Services.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceDefaults(builder.Configuration);

builder.Services.AddSignalR();

builder.Services.AddSingleton<IIntegrationEventHandler<BinanceCryptoAssetPriceTickerIntegrationEvent>, BinanceCryptoAssetPriceTickerIntegrationEventHandler>();

var app = builder.Build();

app.MapHub<NotificationsHub>("/hub/notificationhub");

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<BinanceCryptoAssetPriceTickerIntegrationEvent, IIntegrationEventHandler<BinanceCryptoAssetPriceTickerIntegrationEvent>>();

await app.RunAsync();