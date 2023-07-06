using Utilities;

namespace BlazorApp.Client.Entities
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

        public string GetPriceString()
        {
            var format = CommonUtilities.GetDefaultDisplayFormat(PriceUSDT);
            return PriceUSDT.ToString(format);
        }

        public EmaCrossEntity? GetEmaCrossEntity(string interval)
        {
            lock (_emaCrossDict)
            {
                return _emaCrossDict.ContainsKey(interval) ? _emaCrossDict[interval] : null;
            }
        }

        public bool UpdateEmaCross(EmaCrossEntity emaCrossEntity)
        {
            if (emaCrossEntity == null || string.IsNullOrWhiteSpace(emaCrossEntity.Interval))
                return false;

            if (emaCrossEntity.AssetType != AssetType || emaCrossEntity.BaseAsset != BaseAsset)
                return false;

            lock (_emaCrossDict)
            {
                if (!_emaCrossDict.TryGetValue(emaCrossEntity.Interval, out EmaCrossEntity? emaCross))
                {
                    emaCross = new EmaCrossEntity(AssetType, BaseAsset, emaCrossEntity.Interval);
                    _emaCrossDict.Add(emaCrossEntity.Interval, emaCross);
                }

                emaCross.ValueShort = emaCrossEntity.ValueShort;
                emaCross.ValueLong = emaCrossEntity.ValueLong;
                emaCross.PrevValueShort = emaCrossEntity.PrevValueShort;
                emaCross.PrevValueLong = emaCrossEntity.PrevValueLong;
            }

            return true;
        }
    }
}