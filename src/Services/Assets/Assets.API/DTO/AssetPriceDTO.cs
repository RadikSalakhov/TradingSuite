using Assets.API.Entites;
using Services.Common;

namespace Assets.API.DTO
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

        public static AssetPriceDTO FromEntity(AssetEntity asset)
        {
            if (asset == null)
                throw new ArgumentNullException(nameof(asset));

            return new AssetPriceDTO(asset.AssetType, asset.BaseAsset, CommonConstants.USDT, asset.PriceUSDT);
        }
    }
}