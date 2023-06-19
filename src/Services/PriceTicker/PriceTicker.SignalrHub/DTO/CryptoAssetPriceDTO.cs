namespace PriceTicker.SignalrHub.DTO
{
    public class CryptoAssetPriceDTO
    {
        public string CryptoAsset { get; }

        public string BaseCryptoAsset { get; }

        public decimal Price { get; }

        public CryptoAssetPriceDTO(string cryptoAsset, string baseCryptoAsset, decimal price)
        {
            CryptoAsset = cryptoAsset;
            BaseCryptoAsset = baseCryptoAsset;
            Price = price;
        }
    }
}