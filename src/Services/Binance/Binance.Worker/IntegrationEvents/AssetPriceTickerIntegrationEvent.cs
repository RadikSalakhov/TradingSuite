using EventBus.Events;

namespace Binance.Worker.IntegrationEvents
{
    public class AssetPriceTickerIntegrationEvent : IntegrationEvent
    {
        public string AssetType { get; }

        public string BaseAsset { get; }

        public string QuoteAsset { get; }

        public decimal Price { get; }

        public AssetPriceTickerIntegrationEvent(string assetType, string baseAsset, string quoteAsset, decimal price)
        {
            AssetType = assetType;
            BaseAsset = baseAsset;
            QuoteAsset = quoteAsset;
            Price = price;
        }
    }
}