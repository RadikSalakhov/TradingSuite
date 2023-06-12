using Binance.Domain.Common;

namespace Binance.Domain.Entities
{
    public class PriceTickerEntity : BaseEntity
    {
        public CryptoAsset CryptoAsset { get; set; }

        public CryptoAsset BaseCryptoAsset { get; set; } = CryptoAsset.USDT;

        public decimal Price { get; set; }

        public CryptoSymbol GetCryptoSymbol()
        {
            return CryptoSymbol.CreateFromCryptoAssets(CryptoAsset, BaseCryptoAsset);
        }

        public override string GetDescription()
        {
            return $"{CryptoAsset}: {Price.ToString(GetDisplayFormat())}";
        }

        public string GetDisplayFormat()
        {
            return CryptoAsset.GetAmountDisplayFormat(CryptoAsset, Price);
        }
    }
}