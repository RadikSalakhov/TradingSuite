using Utilities;

namespace BlazorApp.Client.Entities
{
    public class AssetPriceEntity
    {
        public string AssetId { get; }

        public string BaseAssetId { get; }

        public decimal Price { get; }

        public string Symbol { get; }

        public AssetPriceEntity(string assetId, string baseAssetId, decimal price)
        {
            AssetId = assetId;
            BaseAssetId = baseAssetId;
            Price = price;
            Symbol = $"{AssetId}/{BaseAssetId}";
        }

        public string GetPriceString()
        {
            var format = CommonUtilities.GetDefaultDisplayFormat(Price);
            return Price.ToString(format);
        }
    }
}