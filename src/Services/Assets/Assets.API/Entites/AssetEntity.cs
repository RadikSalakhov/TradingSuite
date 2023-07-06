namespace Assets.API.Entites
{
    public class AssetEntity
    {
        private readonly Dictionary<string, EmaCrossEntity> _emaCrossDict = new();

        public string AssetType { get; }

        public string BaseAsset { get; }

        public decimal PriceUSDT { get; set; }

        public AssetEntity(string assetType, string baseAsset)
            : this(assetType, baseAsset, 0m)
        {
        }

        public AssetEntity(string assetType, string baseAsset, decimal priceUSDT)
        {
            AssetType = assetType;
            BaseAsset = baseAsset;
            PriceUSDT = priceUSDT;
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
                    emaCross = new EmaCrossEntity(AssetType, BaseAsset, interval);
                    _emaCrossDict.Add(interval, emaCross);
                }

                emaCross.ValueShort = valueShort;
                emaCross.ValueLong = valueLong;
                emaCross.PrevValueShort = prevValueShort;
                emaCross.PrevValueLong = prevValueLong;
            }
        }
    }
}