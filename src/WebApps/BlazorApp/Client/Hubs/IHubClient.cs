using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorApp.Client.Hubs
{
    public interface IHubClient : IAsyncDisposable
    {
        bool IsConnected { get; }

        HubConnectionState HubConnectionState { get; }

        event Func<Task>? StateChanged;

        Task StartAsync();
    }
}