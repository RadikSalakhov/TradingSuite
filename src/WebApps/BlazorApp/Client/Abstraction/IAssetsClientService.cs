using BlazorApp.Client.DTO;

namespace BlazorApp.Client.Abstraction
{
    public interface IAssetsClientService
    {
        Task<AssetPriceDTO?> GetAssetPriceAsync(string assetType, string baseAsset);

        Task<IEnumerable<EmaCrossDTO>> GetEmaCrossAsync(string assetType, string baseAsset);
    }
}