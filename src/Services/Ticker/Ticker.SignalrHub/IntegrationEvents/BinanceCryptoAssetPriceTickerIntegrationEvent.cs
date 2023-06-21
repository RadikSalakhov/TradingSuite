using EventBus.Events;

namespace Ticker.SignalrHub.IntegrationEvents
{
    public class BinanceCryptoAssetPriceTickerIntegrationEvent : IntegrationEvent
    {
        public string CryptoAsset { get; }

        public string BaseCryptoAsset { get; }

        public decimal Price { get; }

        public BinanceCryptoAssetPriceTickerIntegrationEvent(string cryptoAsset, string baseCryptoAsset, decimal price)
        {
            CryptoAsset = cryptoAsset;
            BaseCryptoAsset = baseCryptoAsset;
            Price = price;
        }
    }
}