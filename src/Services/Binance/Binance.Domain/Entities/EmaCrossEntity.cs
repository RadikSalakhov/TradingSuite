using Binance.Domain.Common;

namespace Binance.Domain.Entities
{
    public class EmaCrossEntity : BaseIndicatorEntity
    {
        public CryptoAsset CryptoAsset { get; set; }

        public string TAInterval { get; set; } = string.Empty;

        public decimal ValueShort { get; set; }

        public decimal ValueLong { get; set; }

        public decimal PrevValueShort { get; set; }

        public decimal PrevValueLong { get; set; }
    }
}