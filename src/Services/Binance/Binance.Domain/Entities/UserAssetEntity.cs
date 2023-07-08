using Binance.Domain.Common;

namespace Binance.Domain.Entities
{
    public class UserAssetEntity : BaseEntity
    {
        public CryptoAsset CryptoAsset { get; set; }

        public decimal Free { get; set; }

        public decimal BtcValuation { get; set; }

        //public AssetStateEntity? AssetState { get; set; }

        public decimal GetUSDTValuation(decimal assetPrice)
        {
            return Free * assetPrice;
        }

        public bool IsAssetActive(decimal assetPrice)
        {
            return GetUSDTValuation(assetPrice) > GeneralConstants.ASSET_MIN_USDT_AMOUNT;
        }

        public override string GetDescription()
        {
            return $"{CryptoAsset}: {CryptoAsset}";
        }
    }
}