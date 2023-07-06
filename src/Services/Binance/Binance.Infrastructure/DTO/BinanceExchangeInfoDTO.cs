using Binance.Domain.Common;
using Newtonsoft.Json;

namespace Binance.Infrastructure.DTO
{
    public class BinanceExchangeInfoDTO : BaseDTO
    {
        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("serverTime")]
        public long ServerTime { get; set; }

        [JsonProperty("symbols")]
        public BinanceExchangeInfoSymbolDTO[] Symbols { get; set; }
    }
}