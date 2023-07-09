using Assets.Application;
using Assets.Infrastructure;
using EventBus.Abstraction;
using Services.Common.Extensions;
using Services.Common.IntegrationEvents;

var corsPolicyName = "assets-api-cors-policy";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServiceDefaults(builder.Configuration);

builder.Services.AddSignalR();

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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

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