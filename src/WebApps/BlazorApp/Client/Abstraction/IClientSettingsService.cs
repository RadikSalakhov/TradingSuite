using BlazorApp.Client.Services.ClientSettingsEntries;

namespace BlazorApp.Client.Abstraction
{
    public interface IClientSettingsService
    {
        WindowSizeClientSettingsEntry WindowSize { get; }
    }
}