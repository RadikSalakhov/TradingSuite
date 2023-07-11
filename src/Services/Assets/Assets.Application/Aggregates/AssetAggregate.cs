using Assets.Domain.Entites;

namespace Assets.Application.Aggregates
{
    public class AssetAggregate
    {
        private readonly Dictionary<string, EmaCrossEntity> _emaCrossDict = new();

        public AssetEntity AssetEntity { get; private set; }

        public AssetPriceEntity AssetPriceEntity { get; private set; }

        private AssetAggregate(AssetEntity assetEntity, AssetPriceEntity assetPriceEntity)
        {
            AssetEntity = assetEntity ?? throw new ArgumentNullException(nameof(assetEntity));
            AssetPriceEntity = assetPriceEntity ?? throw new ArgumentNullException(nameof(assetPriceEntity));
        }

        public IEnumerable<EmaCrossEntity> GetEmaCrossEntities()
        {
            var resultList = new List<EmaCrossEntity>();

            lock (_emaCrossDict)
            {
                foreach (var kvp in _emaCrossDict)
                    resultList.Add(kvp.Value);
            }

            return resultList;
        }

        public void UpdateEmaCross(string interval, decimal valueShort, decimal valueLong, decimal prevValueShort, decimal prevValueLong)
        {
            if (string.IsNullOrWhiteSpace(interval))
                return;

            lock (_emaCrossDict)
            {
                if (!_emaCrossDict.TryGetValue(interval, out EmaCrossEntity? emaCross))
                {
                    emaCross = EmaCrossEntity.Create(AssetEntity.AssetType, AssetEntity.BaseAsset, interval);
                    _emaCrossDict.Add(interval, emaCross);
                }

                emaCross.ValueShort = valueShort;
                emaCross.ValueLong = valueLong;
                emaCross.PrevValueShort = prevValueShort;
                emaCross.PrevValueLong = prevValueLong;
            }
        }

        public static AssetAggregate Create(string assetType, string baseAsset)
        {
            var asset = AssetEntity.Create(assetType, baseAsset);
            var assetPrice = AssetPriceEntity.Create(assetType, baseAsset);

            return new AssetAggregate(asset, assetPrice);
        }
    }
}