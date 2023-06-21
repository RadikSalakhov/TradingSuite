using TaApi.BackgroundTasks.Entities;
using TaApi.BackgroundTasks.Settings;
using TaApi.BackgroundTasks.Structs;

namespace TaApi.BackgroundTasks.DTO
{
    public class BulkResponseDTO<TEntity, TDTO>
        where TEntity : BaseEntity, new()
        where TDTO : BaseDTO<TEntity>
    {
        public IEnumerable<BulkResponseItemDTO<TEntity, TDTO>> data { get; set; }
    }

    public class BulkResponseItemDTO<TEntity, TDTO>
        where TEntity : BaseEntity, new()
        where TDTO : BaseDTO<TEntity>
    {
        public string id { get; set; }

        public TDTO result { get; set; }

        public BulkResponseIdEntity? TryParseId()
        {
            //example: binance_BTC/USDT_1m_ema_50_true_0
            if (string.IsNullOrWhiteSpace(id))
                return null;

            var parts = id.Split('_');
            if (parts.Length < 5)
                return null;

            var exchange = parts[0];
            var symbol = parts[1];
            var intervalStr = parts[2];
            var indicatorStr = parts[3];
            var valueStr = parts[4];

            if (exchange != ConstSettings.TA_API_EXCAHNGE)
                return null;

            var cryptoAssetsTuple = Asset.TryParseTaApiSymbol(symbol);
            if (cryptoAssetsTuple == null)
                return null;

            if (cryptoAssetsTuple.Item1 == Asset._EMPTY || cryptoAssetsTuple.Item2 != Asset.USDT)
                return null;

            var taInterval = TAInterval.TryFind(intervalStr);
            if (taInterval == TAInterval._EMPTY)
                return null;

            var taIndicator = TAIndicator.TryFind(indicatorStr);
            if (taIndicator == TAIndicator._EMPTY)
                return null;

            if (!decimal.TryParse(valueStr, out decimal indicatorValue))
                return null;

            return new BulkResponseIdEntity
            {
                ReferenceDT = DateTime.UtcNow,
                Exchange = exchange,
                Asset = cryptoAssetsTuple.Item1,
                Interval = taInterval,
                Indicator = taIndicator,
                IndicatorValue = indicatorValue
            };
        }
    }
}