namespace TaApi.BackgroundTasks.Data
{
    public struct Asset : IComparable<Asset>
    {
        public string Value { get; set; }

        private Asset(string value)
        {
            Value = value;
        }

        public override string ToString() => Value;

        public int CompareTo(Asset other)
        {
            return Value.CompareTo(other.Value);
        }

        public static implicit operator string(Asset value) => value.Value;

        public static implicit operator Asset(string value) => new(value);

        public static Asset _EMPTY { get => new(string.Empty); }

        public static Asset USDT { get => new("USDT"); }

        public static Tuple<Asset, Asset>? TryParseTaApiSymbol(string taApiSymbol)
        {
            if (string.IsNullOrWhiteSpace(taApiSymbol))
                return null;

            var parts = taApiSymbol.Split('/');
            if (parts == null || parts.Length != 2)
                return null;

            var cryptoAsset = parts[0];
            var baseCryptoAsset = parts[1];

            if (cryptoAsset == _EMPTY || baseCryptoAsset == _EMPTY)
                return null;

            return new Tuple<Asset, Asset>(cryptoAsset, baseCryptoAsset);
        }
    }
}