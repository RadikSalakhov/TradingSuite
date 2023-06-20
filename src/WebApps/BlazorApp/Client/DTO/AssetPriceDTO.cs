using BlazorApp.Client.Entities;

namespace BlazorApp.Client.DTO
{
    public class AssetPriceDTO
    {
        public string Asset { get; }

        public string BaseAsset { get; }

        public decimal Price { get; }

        public AssetPriceDTO(string asset, string baseAsset, decimal price)
        {
            Asset = asset;
            BaseAsset = baseAsset;
            Price = price;
        }

        public AssetPriceEntity ToEntity()
        {
            return new AssetPriceEntity(Asset, BaseAsset, Price);
        }
    }
}