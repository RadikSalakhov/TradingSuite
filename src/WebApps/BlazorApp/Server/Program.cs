using AspNetCore.Proxy;
using BlazorApp.Server;
using BlazorApp.Server.IntegrationEventHandlers;
using BlazorApp.Server.IntegrationEvents;
using EventBus.Abstraction;
using Services.Common.Extensions;

var corsPolicyName = "blazor-app-signalrhub-cors-policy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceDefaults(builder.Configuration);

builder.Services.AddSignalR();

builder.Services.AddSingleton<IIntegrationEventHandler<BinanceCryptoAssetPriceTickerIntegrationEvent>, BinanceCryptoAssetPriceTickerIntegrationEventHandler>();
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

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(corsPolicyName);

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.MapHub<NotificationsHub>("/hub");

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<BinanceCryptoAssetPriceTickerIntegrationEvent, IIntegrationEventHandler<BinanceCryptoAssetPriceTickerIntegrationEvent>>();
eventBus.Subscribe<BinanceServerTimeIntegrationEvent, IIntegrationEventHandler<BinanceServerTimeIntegrationEvent>>();
eventBus.Subscribe<IndicatorEmaCrossIntegrationEvent, IIntegrationEventHandler<IndicatorEmaCrossIntegrationEvent>>();

app.Run();