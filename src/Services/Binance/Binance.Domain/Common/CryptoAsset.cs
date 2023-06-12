namespace Binance.Domain.Common
{
    public struct CryptoAsset : IComparable<CryptoAsset>
    {
        #region Main

        public string Value { get; set; }

        private CryptoAsset(string value)
        {
            Value = value;
        }

        public override string ToString() => Value;

        public int CompareTo(CryptoAsset other)
        {
            return Value.CompareTo(other.Value);
        }

        public static implicit operator string(CryptoAsset value) => value.Value;

        public static implicit operator CryptoAsset(string value) => new(value);

        #endregion

        #region Public Static Values

        public static CryptoAsset _EMPTY { get => new(string.Empty); }
        public static CryptoAsset ADA { get => new("ADA"); }
        public static CryptoAsset APT { get => new("APT"); }
        public static CryptoAsset ARB { get => new("ARB"); }
        public static CryptoAsset ATOM { get => new("ATOM"); }
        public static CryptoAsset AVAX { get => new("AVAX"); }
        public static CryptoAsset BNB { get => new("BNB"); }
        public static CryptoAsset BTC { get => new("BTC"); }
        public static CryptoAsset CAKE { get => new("CAKE"); }
        public static CryptoAsset CFX { get => new("CFX"); }
        public static CryptoAsset DOGE { get => new("DOGE"); }
        public static CryptoAsset DOT { get => new("DOT"); }
        public static CryptoAsset ETH { get => new("ETH"); }
        public static CryptoAsset FIL { get => new("FIL"); }
        public static CryptoAsset FTM { get => new("FTM"); }
        public static CryptoAsset ICP { get => new("ICP"); }
        public static CryptoAsset ID { get => new("ID"); }
        public static CryptoAsset INJ { get => new("INJ"); }
        public static CryptoAsset LINK { get => new("LINK"); }
        public static CryptoAsset LTC { get => new("LTC"); }
        public static CryptoAsset MANA { get => new("MANA"); }
        public static CryptoAsset MATIC { get => new("MATIC"); }
        public static CryptoAsset OP { get => new("OP"); }
        public static CryptoAsset RNDR { get => new("RNDR"); }
        public static CryptoAsset SAND { get => new("SAND"); }
        public static CryptoAsset SHIB { get => new("SHIB"); }
        public static CryptoAsset SOL { get => new("SOL"); }
        public static CryptoAsset TRX { get => new("TRX"); }
        public static CryptoAsset UNI { get => new("UNI"); }
        public static CryptoAsset XLM { get => new("XLM"); }
        public static CryptoAsset XMR { get => new("XMR"); }
        public static CryptoAsset XRP { get => new("XRP"); }
        public static CryptoAsset USDT { get => new("USDT"); }

        #endregion

        #region Public Static Methods

        public static decimal GetLotSizeUSDT(CryptoAsset cryptoAsset)
        {
            //https://api.binance.com/api/v3/exchangeInfo?symbol=BTCUSDT
            if (cryptoAsset == ADA) return 0.1m;
            else if (cryptoAsset == APT) return 0.01m;
            else if (cryptoAsset == ARB) return 0.1m;
            else if (cryptoAsset == ATOM) return 0.01m;
            else if (cryptoAsset == AVAX) return 0.01m;
            else if (cryptoAsset == BNB) return 0.001m;
            else if (cryptoAsset == BTC) return 0.00001m;
            else if (cryptoAsset == CAKE) return 0.01m;
            else if (cryptoAsset == CFX) return 1m;
            else if (cryptoAsset == DOGE) return 1m;
            else if (cryptoAsset == DOT) return 0.01m;
            else if (cryptoAsset == ETH) return 0.0001m;
            else if (cryptoAsset == FIL) return 0.01m;
            else if (cryptoAsset == FTM) return 1m;
            else if (cryptoAsset == ICP) return 0.01m;
            else if (cryptoAsset == ID) return 1m;
            else if (cryptoAsset == INJ) return 0.1m;
            else if (cryptoAsset == LINK) return 0.01m;
            else if (cryptoAsset == LTC) return 0.001m;
            else if (cryptoAsset == MANA) return 1m;
            else if (cryptoAsset == MATIC) return 0.1m;
            else if (cryptoAsset == OP) return 0.01m;
            else if (cryptoAsset == RNDR) return 0.01m;
            else if (cryptoAsset == SAND) return 1m;
            else if (cryptoAsset == SHIB) return 1m;
            else if (cryptoAsset == SOL) return 0.01m;
            else if (cryptoAsset == TRX) return 0.1m;
            else if (cryptoAsset == UNI) return 0.01m;
            else if (cryptoAsset == XLM) return 1m;
            else if (cryptoAsset == XMR) return 0.001m;
            else if (cryptoAsset == XRP) return 1m;
            else
                throw new Exception($"Th following CryptoAsset has no precision decimals specified: {cryptoAsset}");
        }

        public static string GetAmountFormatted(CryptoAsset cryptoAsset, decimal value)
        {
            var format = GetAmountDisplayFormat(cryptoAsset, value);
            return value.ToString(format);
        }

        public static string GetAmountDisplayFormat(CryptoAsset cryptoAsset, decimal value)
        {
            if (value >= 1000m)
                return "0";
            else if (value >= 100m)
                return "0.0";
            else if (value >= 10m)
                return "0.00";
            else if (value >= 1m)
                return "0.000";
            else
                return "0.0000";
        }

        public static string GetPriceDisplayFormat(CryptoAsset cryptoAsset, decimal value)
        {
            if (cryptoAsset == SHIB)
                return "0.000000";

            return GetAmountDisplayFormat(cryptoAsset, value);
        }

        public static IEnumerable<CryptoAsset> GetAll(bool skipUSDT = false)
        {
            yield return ADA;
            yield return APT;
            yield return ARB;
            yield return ATOM;
            yield return AVAX;
            yield return BNB;
            yield return BTC;
            yield return CAKE;
            yield return CFX;
            yield return DOGE;
            yield return DOT;
            yield return ETH;
            yield return FIL;
            yield return FTM;
            yield return ICP;
            yield return ID;
            yield return INJ;
            yield return LINK;
            yield return LTC;
            yield return MANA;
            yield return MATIC;
            yield return OP;
            yield return RNDR;
            yield return SAND;
            yield return SHIB;
            yield return SOL;
            yield return TRX;
            yield return UNI;
            yield return XLM;
            yield return XMR;
            yield return XRP;

            if (!skipUSDT)
                yield return USDT;
        }

        public static CryptoAsset TryFind(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return _EMPTY;

            var valueUpper = value.ToUpper();

            foreach (var cryptoAsset in GetAll())
            {
                if (cryptoAsset.Value == valueUpper)
                    return cryptoAsset;
            }

            return _EMPTY;
        }

        #endregion
    }
}