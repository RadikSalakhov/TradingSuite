namespace Binance.Domain.Entities
{
    public class AssetEntity
    {
        public string AssetType { get; } = "CRYPTO";

        public string BaseAsset { get; set; } = string.Empty;

        public decimal LotStepSize { get; set; }
    }
}