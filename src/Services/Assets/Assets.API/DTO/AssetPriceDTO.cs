using Assets.Domain.Entites;
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

        public static AssetPriceDTO FromEntity(AssetPriceEntity assetPriceEntity)
        {
            if (assetPriceEntity == null)
                throw new ArgumentNullException(nameof(assetPriceEntity));

            return new AssetPriceDTO(assetPriceEntity.AssetType, assetPriceEntity.BaseAsset, CommonConstants.USDT, assetPriceEntity.PriceUSDT);
        }
    }
}