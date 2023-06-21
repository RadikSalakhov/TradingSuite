namespace TaApi.BackgroundTasks.Structs
{
    public struct TAInterval : IComparable<TAInterval>
    {
        #region Main

        public string Value { get; private set; }

        public TAInterval(string value)
        {
            Value = value;
        }

        public int CompareTo(TAInterval other)
        {
            return Value.CompareTo(other.Value);
        }

        public override string ToString() => Value;

        public static implicit operator string(TAInterval value) => value.Value;

        #endregion

        #region Public Static Values

        public static TAInterval _EMPTY { get => new(string.Empty); }
        public static TAInterval Interval_1s { get => new("1s"); }
        public static TAInterval Interval_1m { get => new("1m"); }
        public static TAInterval Interval_5m { get => new("5m"); }
        public static TAInterval Interval_15m { get => new("15m"); }
        public static TAInterval Interval_30m { get => new("30m"); }
        public static TAInterval Interval_1h { get => new("1h"); }
        public static TAInterval Interval_2h { get => new("2h"); }
        public static TAInterval Interval_4h { get => new("4h"); }
        public static TAInterval Interval_12h { get => new("12h"); }
        public static TAInterval Interval_1d { get => new("1d"); }
        public static TAInterval Interval_1w { get => new("1w"); }

        #endregion

        #region Public Static Methods

        public static IEnumerable<TAInterval> GetAll()
        {
            yield return Interval_1s;
            yield return Interval_1m;
            yield return Interval_5m;
            yield return Interval_15m;
            yield return Interval_30m;
            yield return Interval_1h;
            yield return Interval_2h;
            yield return Interval_4h;
            yield return Interval_12h;
            yield return Interval_1d;
            yield return Interval_1w;
        }

        public static TAInterval TryFind(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return _EMPTY;

            var valueLower = value.ToLower();

            foreach (var taInterval in GetAll())
            {
                if (taInterval.Value == valueLower)
                    return taInterval;
            }

            return _EMPTY;
        }

        #endregion
    }
}