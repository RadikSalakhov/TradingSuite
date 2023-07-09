using Binance.Domain.Common;
using Newtonsoft.Json;

namespace Binance.Infrastructure.DTO
{
    public class BinanceExchangeInfoSymbolFilterDTO : BaseDTO
    {
        [JsonProperty("filterType")]
        public string FilterType { get; set; }

        [JsonProperty("minQty")]
        public decimal MinQty { get; set; }

        [JsonProperty("maxQty")]
        public decimal MaxQty { get; set; }

        [JsonProperty("stepSize")]
        public decimal StepSize { get; set; }
    }
}