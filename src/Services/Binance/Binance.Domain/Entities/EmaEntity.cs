using Binance.Domain.Common;

namespace Binance.Domain.Entities
{
    public class EmaEntity : BaseIndicatorEntity
    {
        public decimal Value { get; set; }
    }
}