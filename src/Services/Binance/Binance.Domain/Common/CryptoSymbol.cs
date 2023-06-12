namespace Binance.Domain.Common
{
    public struct CryptoSymbol
    {
        #region Main

        public string Value { get; set; }

        public CryptoSymbol(string value)
        {
            Value = value;
        }

        public override string ToString() => Value;

        public static implicit operator string(CryptoSymbol value) => value.Value;

        public static implicit operator CryptoSymbol(string value) => new(value);

        #endregion

        #region Public Static Methods

        public static CryptoSymbol CreateFromCryptoAsset(CryptoAsset cryptoAsset)
        {
            return CreateFromCryptoAssets(cryptoAsset, CryptoAsset.USDT);
        }

        public static CryptoSymbol CreateFromCryptoAssets(CryptoAsset cryptoAsset, CryptoAsset baseCryptoAsset)
        {
            return new CryptoSymbol($"{cryptoAsset}{baseCryptoAsset}");
        }

        public static Tuple<CryptoAsset, CryptoAsset>? TryParse(CryptoSymbol cryptoSymbol)
        {
            if (cryptoSymbol.Value.EndsWith(CryptoAsset.USDT))
            {
                var cryptoAsset = cryptoSymbol.Value.Replace(CryptoAsset.USDT, string.Empty);
                if (!string.IsNullOrWhiteSpace(cryptoAsset))
                    return new Tuple<CryptoAsset, CryptoAsset>(cryptoAsset, CryptoAsset.USDT);
            }

            return null;
        }

        public static string GetDisplayFormat(CryptoSymbol cryptoSymbol)
        {
            return "0.0";
        }

        #endregion
    }
}