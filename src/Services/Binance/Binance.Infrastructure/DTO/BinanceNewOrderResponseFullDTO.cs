using Binance.Domain.Common;
using Binance.Domain.Entities;
using Newtonsoft.Json;

namespace Binance.Infrastructure.DTO
{
    public class BinanceNewOrderResponseFullDTO : BaseDTO<SpotTradingOrderEntity>
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("orderId")]
        public long OrderId { get; set; }

        [JsonProperty("orderListId")]
        public long OrderListId { get; set; }

        [JsonProperty("clientOrderId")]
        public string ClientOrderId { get; set; }

        [JsonProperty("transactTime")]
        public long TransactTime { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("origQty")]
        public decimal OrigQty { get; set; }

        [JsonProperty("executedQty")]
        public decimal ExecutedQty { get; set; }

        [JsonProperty("cummulativeQuoteQty")]
        public decimal CummulativeQuoteQty { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("timeInForce")]
        public string TimeInForce { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("side")]
        public string Side { get; set; }

        [JsonProperty("strategyId")]
        public long strategyId { get; set; }

        [JsonProperty("strategyType")]
        public long StrategyType { get; set; }

        [JsonProperty("workingTime")]
        public long WorkingTime { get; set; }

        [JsonProperty("selfTradePreventionMode")]
        public string SelfTradePreventionMode { get; set; }

        [JsonProperty("fills")]
        public BinanceNewOrderResponseFullFillDTO[] Fills { get; set; }

        public override SpotTradingOrderEntity GetEntity()
        {
            var quantity = 0m;
            var averagePrice = 0m;
            var comission = 0m;
            var commissionCryptoAsset = CryptoAsset._EMPTY;

            if (Fills != null && Fills.Any())
            {
                quantity = Fills.Sum(v => v.Qty);
                foreach (var fill in Fills)
                {
                    var coeff = fill.Qty / quantity;
                    averagePrice += coeff * fill.Price;
                }

                var firstCommissionAsset = Fills.First().CommissionAsset;
                if (Fills.All(v => v.CommissionAsset == firstCommissionAsset))
                {
                    commissionCryptoAsset = firstCommissionAsset;
                    comission = Fills.Sum(v => v.Commission);
                }
            }

            var entity = BaseEntity.CreateNew<SpotTradingOrderEntity>();

            entity.CryptoSymbol = Symbol;
            entity.Price = averagePrice;
            entity.Quantity = quantity;
            entity.CommissionCryptoAsset = commissionCryptoAsset;
            entity.Commission = comission;

            return entity;
        }
    }
}