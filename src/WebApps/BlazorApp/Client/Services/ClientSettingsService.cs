using BlazorApp.Client.Abstraction;
using BlazorApp.Client.Services.ClientSettingsEntries;
using System.Drawing;

namespace BlazorApp.Client.Services
{
    public class ClientSettingsService : IClientSettingsService
    {
        public WindowSizeClientSettingsEntry WindowSize { get; } = new WindowSizeClientSettingsEntry();
    }
}