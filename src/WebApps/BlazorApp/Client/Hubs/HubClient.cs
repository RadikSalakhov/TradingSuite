using BlazorApp.Client.Configuration;
using BlazorApp.Client.DTO;
using BlazorApp.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;

namespace BlazorApp.Client.Hubs
{
    public class HubClient : IHubClient
    {
        private readonly ClientOptions _clientOptions;

        private readonly IClientCacheService _clientCacheService;

        private HubConnection? _hubConnection;

        public event Func<Task>? StateChanged;

        public HubConnectionState HubConnectionState
        {
            get
            {
                return _hubConnection?.State ?? HubConnectionState.Disconnected;
            }
        }

        public HubClient(IOptions<ClientOptions> clientOptions, IClientCacheService clientCacheService)
        {
            _clientOptions = clientOptions.Value;
            _clientCacheService = clientCacheService;
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection != null)
            {
                _hubConnection.Closed -= hubConnection_Closed;
                _hubConnection.Reconnecting -= hubConnection_Reconnecting;
                _hubConnection.Reconnected -= hubConnection_Reconnected;

                await _hubConnection.DisposeAsync();
                _hubConnection = null;
            }
        }

        public bool IsConnected => _hubConnection?.State == HubConnectionState.Connected;

        public async Task StartAsync()
        {
            if (_hubConnection != null)
                await _hubConnection.DisposeAsync();

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_clientOptions.SignalrHubUrl)
                .Build();

            _hubConnection.Closed += hubConnection_Closed;
            _hubConnection.Reconnecting += hubConnection_Reconnecting;
            _hubConnection.Reconnected += hubConnection_Reconnected;

            _hubConnection.On<ServerTimeDTO>(nameof(ServerTimeDTO), dto => _clientCacheService.UpdateServerTime(dto.ServerTime));

            //_hubConnection.On<IEnumerable<AssetEntity>>(TradingHubMessage.Assets, entities => TradingServiceClient.Cache.UpdateAssets(entities));

            //_hubConnection.On<IEnumerable<PriceTickerEntity>>(TradingHubMessage.PriceTickers, entities => TradingServiceClient.Cache.UpdatePriceTickers(entities));

            //_hubConnection.On<GeneralStatusEntity>(TradingHubMessage.GeneralStatus, status => TradingServiceClient.Cache.UpdateGeneralStatus(status));

            //_hubConnection.On<IEnumerable<EmaCrossEntity>>(TradingHubMessage.IndicatorEmaCross, entities => TradingServiceClient.Cache.UpdateEmaCrossEntites(entities));

            await _hubConnection.StartAsync();
        }

        private async Task hubConnection_Closed(Exception? arg)
        {
            if (StateChanged != null)
                await StateChanged.Invoke();
        }

        private async Task hubConnection_Reconnecting(Exception? arg)
        {
            if (StateChanged != null)
                await StateChanged.Invoke();
        }

        private async Task hubConnection_Reconnected(string? arg)
        {
            if (StateChanged != null)
                await StateChanged.Invoke();
        }
    }
}