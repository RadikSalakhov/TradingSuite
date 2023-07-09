using Assets.Domain.Entites;

namespace Assets.WebApi.DTO
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

        public static EmaCrossDTO FromEntity(EmaCrossEntity asset)
        {
            if (asset == null)
                throw new ArgumentNullException(nameof(asset));

            return new EmaCrossDTO(asset.AssetType, asset.BaseAsset, asset.Interval, asset.ValueShort, asset.ValueLong, asset.PrevValueShort, asset.PrevValueLong);
        }
    }
}