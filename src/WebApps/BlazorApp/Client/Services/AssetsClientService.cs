using BlazorApp.Client.Abstraction;
using BlazorApp.Client.DTO;
using System.Net.Http.Json;

namespace BlazorApp.Client.Services
{
    public class AssetsClientService : IAssetsClientService
    {
        private const string ASSETS_API = "assets-api";

        private readonly HttpClient _httpClient;

        public AssetsClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AssetPriceDTO?> GetAssetPriceAsync(string assetType, string baseAsset)
        {
            var result = await _httpClient.GetFromJsonAsync<AssetPriceDTO>($"{ASSETS_API}/assets/get-asset-price?assetType={assetType}&baseAsset={baseAsset}");

            return result;
        }

        public async Task<IEnumerable<EmaCrossDTO>> GetEmaCrossAsync(string assetType, string baseAsset)
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<EmaCrossDTO>>($"{ASSETS_API}/assets/get-ema-cross?assetType={assetType}&baseAsset={baseAsset}");

            return result ?? new List<EmaCrossDTO>();
        }
    }
}