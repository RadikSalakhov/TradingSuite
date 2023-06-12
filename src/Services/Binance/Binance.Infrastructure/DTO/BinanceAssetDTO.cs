using Binance.Domain.Common;
using Binance.Domain.Entities;
using Newtonsoft.Json;

namespace Binance.Infrastructure.DTO
{
    public class BinanceAssetDTO : BaseDTO<AssetEntity>
    {
        [JsonProperty("asset")]
        public string Asset { get; set; }

        [JsonProperty("free")]
        public decimal Free { get; set; }

        [JsonProperty("locked")]
        public decimal Locked { get; set; }

        [JsonProperty("freeze")]
        public decimal Freeze { get; set; }

        [JsonProperty("withdrawing")]
        public decimal Withdrawing { get; set; }

        [JsonProperty("ipoable")]
        public decimal IpoAble { get; set; }

        [JsonProperty("btcValuation")]
        public decimal BtcValuation { get; set; }

        public override AssetEntity GetEntity()
        {
            var entity = BaseEntity.CreateNew<AssetEntity>();

            entity.CryptoAsset = Asset;
            entity.Free = Free;
            entity.BtcValuation = BtcValuation;

            return entity;
        }
    }
}