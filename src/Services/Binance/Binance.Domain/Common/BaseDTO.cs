using System.Globalization;

namespace Binance.Domain.Common
{
    public abstract class BaseDTO
    {
        protected static DateTime ConvertDateTimeFromString(string str)
        {
            return long.TryParse(str, out long timestampMS)
                ? ConvertDateTimeFromTimestampMS(timestampMS)
                : DateTime.MinValue;
        }

        protected static DateTime ConvertDateTimeFromTimestampMS(long timestampMS)
        {
            return new DateTime(1970, 1, 1).AddMilliseconds(timestampMS);
        }

        protected static DateTime ConvertDateTimeFromTimestampS(long timestampS)
        {
            return new DateTime(1970, 1, 1).AddSeconds(timestampS);
        }

        protected static decimal ParseDecimal(string str)
        {
            return decimal.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result) ? result : 0m;
        }
    }

    public abstract class BaseDTO<TEntity> : BaseDTO
       where TEntity : BaseEntity, new()
    {
        public abstract TEntity GetEntity();
    }
}