using Binance.Domain.Common;
using Binance.Domain.Entities;
using Newtonsoft.Json;

namespace Binance.Infrastructure.DTO
{
    public class BinancePriceTickerDTO : BaseDTO<PriceTickerEntity>
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        public override PriceTickerEntity GetEntity()
        {
            var entity = BaseEntity.CreateNew<PriceTickerEntity>();

            var cryptoAssetsTuple = CryptoSymbol.TryParse(Symbol);

            entity.CryptoAsset = cryptoAssetsTuple != null ? cryptoAssetsTuple.Item1 : CryptoAsset._EMPTY;
            entity.Price = Price;

            return entity;
        }
    }
}