using Binance.Domain.Common;

namespace Binance.Domain.Entities
{
    public class PriceTickerEntity : BaseEntity
    {
        public CryptoAsset CryptoAsset { get; set; }

        public CryptoAsset QuoteCryptoAsset { get; set; } = CryptoAsset.USDT;

        public decimal Price { get; set; }

        public CryptoSymbol GetCryptoSymbol()
        {
            return CryptoSymbol.CreateFromCryptoAssets(CryptoAsset, QuoteCryptoAsset);
        }

        public override string GetDescription()
        {
            return $"{CryptoAsset}: {Price}";
        }
    }
}