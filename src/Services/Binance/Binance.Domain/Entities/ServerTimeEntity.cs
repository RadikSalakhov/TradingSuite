using Binance.Domain.Common;

namespace Binance.Domain.Entities
{
    public class ServerTimeEntity : BaseEntity
    {
        public DateTime ServerTime { get; set; }

        public static ServerTimeEntity GetDefault()
        {
            return new ServerTimeEntity();
        }
    }
}