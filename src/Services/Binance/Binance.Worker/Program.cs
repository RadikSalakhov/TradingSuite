using Binance.Worker;
using Binance.Infrastructure.Extensions;
using Services.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceDefaults(builder.Configuration);

builder.Services.AddBinanceServices(builder.Configuration);

builder.Services.AddHostedService<Worker>();

var app = builder.Build();

app.Run();