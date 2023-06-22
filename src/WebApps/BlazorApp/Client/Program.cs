using BlazorApp.Client;
using BlazorApp.Client.Abstraction;
using BlazorApp.Client.Configuration;
using BlazorApp.Client.Hubs;
using BlazorApp.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IBrowserService, BrowserService>();

builder.Services.Configure<ClientOptions>(builder.Configuration);

builder.Services.AddSingleton<IClientSettingsService, ClientSettingsService>();

builder.Services.AddSingleton<IClientCacheService, ClientCacheService>();

builder.Services.AddSingleton<IHubClient, HubClient>();

await builder.Build().RunAsync();