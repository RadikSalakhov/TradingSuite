using Binance.Domain.Common;
using Binance.Domain.Entities;
using Newtonsoft.Json;

namespace Binance.Infrastructure.DTO
{
    public class BinanceServerTimeDTO : BaseDTO<ServerTimeEntity>
    {
        [JsonProperty("serverTime")]
        public string ServerTime { get; set; } = string.Empty;

        public override ServerTimeEntity GetEntity()
        {
            var entity = BaseEntity.CreateNew<ServerTimeEntity>();

            entity.ServerTime = ConvertDateTimeFromString(ServerTime);

            return entity;
        }
    }
}