using BlazorApp.Client.Entities;

namespace BlazorApp.Client.DTO
{
    public class EmaCrossDTO
    {
        public string AssetType { get; }

        public string BaseAsset { get; }

        public string Interval { get; }

        public decimal ValueShort { get; }

        public decimal ValueLong { get; }

        public decimal PrevValueShort { get; }

        public decimal PrevValueLong { get; }

        public EmaCrossDTO(string assetType, string baseAsset, string interval, decimal valueShort, decimal valueLong, decimal prevValueShort, decimal prevValueLong)
        {
            AssetType = assetType;
            BaseAsset = baseAsset;
            Interval = interval;
            ValueShort = valueShort;
            ValueLong = valueLong;
            PrevValueShort = prevValueShort;
            PrevValueLong = prevValueLong;
        }

        public EmaCrossEntity ToEntity()
        {
            return new EmaCrossEntity(AssetType, BaseAsset, Interval, ValueShort, ValueLong, PrevValueShort, PrevValueLong);
        }
    }
}