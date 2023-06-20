using Utilities;

namespace BlazorApp.Client.Entities
{
    public class AssetPriceEntity
    {
        public string AssetName { get; }

        public string BaseAssetName { get; }

        public decimal Price { get; }

        public string Symbol { get; }

        public AssetPriceEntity(string assetName, string baseAssetName, decimal price)
        {
            AssetName = assetName;
            BaseAssetName = baseAssetName;
            Price = price;
            Symbol = $"{AssetName}/{BaseAssetName}";
        }

        public string GetPriceString()
        {
            var format = CommonUtilities.GetDefaultDisplayFormat(Price);
            return Price.ToString(format);
        }
    }
}