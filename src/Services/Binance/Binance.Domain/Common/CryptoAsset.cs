namespace Binance.Domain.Common
{
    public struct CryptoAsset : IComparable<CryptoAsset>
    {
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

        public static CryptoAsset _EMPTY { get => new(string.Empty); }
        public static CryptoAsset BNB { get => new("BNB"); }
        public static CryptoAsset USDT { get => new("USDT"); }

        public static IEnumerable<CryptoAsset> GetAll(bool skipUSDT = false)
        {
            yield return BNB;

            if (!skipUSDT)
                yield return USDT;
        }
    }
}