using Binance.Domain.Common;
using Binance.Domain.Entities;
using Newtonsoft.Json;

namespace Binance.Infrastructure.DTO
{
    public class BinanceExchangeInfoSymbolDTO : BaseDTO
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("baseAsset")]
        public string BaseAsset { get; set; }

        [JsonProperty("baseAssetPrecision")]
        public int BaseAssetPrecision { get; set; }

        [JsonProperty("quoteAsset")]
        public string QuoteAsset { get; set; }

        [JsonProperty("quotePrecision")]
        public int QuotePrecision { get; set; }

        [JsonProperty("icebergAllowed")]
        public bool IcebergAllowed { get; set; }

        [JsonProperty("ocoAllowed")]
        public bool OcoAllowed { get; set; }

        [JsonProperty("quoteOrderQtyMarketAllowed")]
        public bool QuoteOrderQtyMarketAllowed { get; set; }

        [JsonProperty("allowTrailingStop")]
        public bool AllowTrailingStop { get; set; }

        [JsonProperty("cancelReplaceAllowed")]
        public bool CancelReplaceAllowed { get; set; }

        [JsonProperty("isSpotTradingAllowed")]
        public bool IsSpotTradingAllowed { get; set; }

        [JsonProperty("isMarginTradingAllowed")]
        public bool IsMarginTradingAllowed { get; set; }

        [JsonProperty("filters")]
        public BinanceExchangeInfoSymbolFilterDTO[] Filters { get; set; }

        public decimal GetLotStepSizeOrDefault()
        {
            var lotSizeFilter = Filters?.Where(v => v.FilterType == "LOT_SIZE").FirstOrDefault();
            return lotSizeFilter != null && lotSizeFilter.StepSize > 0m
                ? lotSizeFilter.StepSize
                : 1m;
        }

        public AssetEntity ToAssetEntity()
        {
            return new AssetEntity
            {
                BaseAsset = BaseAsset,
                LotStepSize = GetLotStepSizeOrDefault()
            };
        }
    }
}