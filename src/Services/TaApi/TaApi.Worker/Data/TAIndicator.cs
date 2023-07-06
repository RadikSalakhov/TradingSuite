namespace TaApi.Worker.Data
{
    public struct TAIndicator : IComparable<TAIndicator>
    {
        #region Main

        public string Value { get; private set; }

        public TAIndicator(string value)
        {
            Value = value;
        }

        public int CompareTo(TAIndicator other)
        {
            return Value.CompareTo(other.Value);
        }

        public override string ToString() => Value;

        public static implicit operator string(TAIndicator value) => value.Value;

        #endregion

        #region Public Static Values

        public static TAIndicator _EMPTY { get => new(string.Empty); }

        public static TAIndicator EMA { get => new("ema"); }

        #endregion

        #region Public Static Methods

        public static IEnumerable<TAIndicator> GetAll()
        {
            yield return EMA;
        }

        public static TAIndicator TryFind(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return _EMPTY;

            var valueLower = value.ToLower();

            foreach (var taIndicator in GetAll())
            {
                if (taIndicator.Value == valueLower)
                    return taIndicator;
            }

            return _EMPTY;
        }

        #endregion
    }
}