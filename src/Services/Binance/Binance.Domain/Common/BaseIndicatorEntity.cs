namespace Binance.Domain.Common
{
    public abstract class BaseIndicatorEntity : BaseEntity
    {
        public DateTime ReferenceDT { get; set; }
    }
}