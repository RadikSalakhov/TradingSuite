using Newtonsoft.Json;

namespace Binance.Infrastructure.DTO
{
    public class BinanceNewOrderResponseFullFillDTO
    {
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("qty")]
        public decimal Qty { get; set; }

        [JsonProperty("commission")]
        public decimal Commission { get; set; }

        [JsonProperty("commissionAsset")]
        public string CommissionAsset { get; set; }

        [JsonProperty("tradeId")]
        public long TradeId { get; set; }
    }
}