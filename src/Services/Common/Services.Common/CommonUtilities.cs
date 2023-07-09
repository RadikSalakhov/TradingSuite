namespace Services.Common
{
    public static class CommonUtilities
    {
        //https://visualrecode.com/blog/csharp-decimals-in-grpc/
        private const decimal DecimalNanoFactor = 1_000_000_000;

        public static decimal ToDecimal(long units, int nanos)
        {
            return units + nanos / DecimalNanoFactor;
        }

        public static Tuple<long, int> FromDecimal(decimal value)
        {
            var units = decimal.ToInt64(value);
            var nanos = decimal.ToInt32((value - units) * DecimalNanoFactor);
            return new Tuple<long, int>(units, nanos);
        }
    }
}