using EventBus.Events;

namespace Ticker.SignalrHub.IntegrationEvents
{
    public class BinanceCryptoAssetPriceTickerIntegrationEvent : IntegrationEvent
    {
        public string CryptoAsset { get; }

        public string QuoteCryptoAsset { get; }

        public decimal Price { get; }

        public BinanceCryptoAssetPriceTickerIntegrationEvent(string cryptoAsset, string quoteCryptoAsset, decimal price)
        {
            CryptoAsset = cryptoAsset;
            QuoteCryptoAsset = quoteCryptoAsset;
            Price = price;
        }
    }
}