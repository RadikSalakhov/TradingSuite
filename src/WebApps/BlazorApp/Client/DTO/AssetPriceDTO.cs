using BlazorApp.Client.Entities;

namespace BlazorApp.Client.DTO
{
    public class AssetPriceDTO
    {
        public string AssetType { get; }

        public string BaseAsset { get; }

        public string QuoteAsset { get; }

        public decimal Price { get; }

        public AssetPriceDTO(string assetType, string baseAsset, string quoteAsset, decimal price)
        {
            AssetType = assetType;
            BaseAsset = baseAsset;
            QuoteAsset = quoteAsset;
            Price = price;
        }
    }
}