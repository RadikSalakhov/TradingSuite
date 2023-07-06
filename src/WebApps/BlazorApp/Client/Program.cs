using BlazorApp.Client;
using BlazorApp.Client.Abstraction;
using BlazorApp.Client.Hubs;
using BlazorApp.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.Configure<ClientOptions>(builder.Configuration);

//Scoped
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IBrowserService, BrowserService>();

builder.Services.AddScoped<IAssetsClientService, AssetsClientService>();

//Singleton
builder.Services.AddSingleton<IClientSettingsService, ClientSettingsService>();

builder.Services.AddSingleton<IClientCacheService, ClientCacheService>();

builder.Services.AddSingleton<IHubClient, HubClient>();

await builder.Build().RunAsync();