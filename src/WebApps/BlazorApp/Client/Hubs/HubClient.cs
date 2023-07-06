using BlazorApp.Client.Abstraction;
using BlazorApp.Client.Configuration;
using BlazorApp.Client.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;

namespace BlazorApp.Client.Hubs
{
    public class HubClient : IHubClient
    {
        private readonly NavigationManager _navigationManager;

        //private readonly ClientOptions _clientOptions;

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

        public HubClient(NavigationManager navigationManager, /*IOptions<ClientOptions> clientOptions,*/ IClientCacheService clientCacheService)
        {
            _navigationManager = navigationManager;
            //_clientOptions = clientOptions.Value;
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

            //var uriBuilder = new UriBuilder(_navigationManager.BaseUri)
            //{
            //    //Port = _clientOptions.SignalrHubPort,
            //    Path = "hub"
            //};

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_navigationManager.ToAbsoluteUri("hub"))
                //.WithUrl(uriBuilder.ToString())
                .Build();

            _hubConnection.Closed += hubConnection_Closed;
            _hubConnection.Reconnecting += hubConnection_Reconnecting;
            _hubConnection.Reconnected += hubConnection_Reconnected;

            _hubConnection.On<ServerTimeDTO>(nameof(ServerTimeDTO), async dto => await _clientCacheService.ServerTime.UpdateAsync(dto.ToEntity()));

            _hubConnection.On<AssetPriceDTO>(nameof(AssetPriceDTO), async dto =>
            {
                await _clientCacheService.Asset.UpdateAssetPriceAsync(dto.AssetType, dto.BaseAsset, dto.QuoteAsset, dto.Price);
            });

            _hubConnection.On<EmaCrossDTO>(nameof(EmaCrossDTO), async dto => await _clientCacheService.Asset.UpdateEmaCrossAsync(dto.ToEntity()));

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