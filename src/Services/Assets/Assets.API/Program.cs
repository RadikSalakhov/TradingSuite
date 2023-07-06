using Assets.API.Abstraction;
using Assets.API.IntegrationEventHandlers;
using Assets.API.Services;
using EventBus.Abstraction;
using Services.Common.Extensions;
using Services.Common.IntegrationEvents;

var corsPolicyName = "assets-api-cors-policy";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServiceDefaults(builder.Configuration);

builder.Services.AddSignalR();

builder.Services.AddSingleton<IIntegrationEventHandler<AssetPriceTickerIntegrationEvent>, AssetPriceTickerIntegrationEventHandler>();
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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICacheService, CacheService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicyName);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<AssetPriceTickerIntegrationEvent, IIntegrationEventHandler<AssetPriceTickerIntegrationEvent>>();
eventBus.Subscribe<IndicatorEmaCrossIntegrationEvent, IIntegrationEventHandler<IndicatorEmaCrossIntegrationEvent>>();

app.Run();