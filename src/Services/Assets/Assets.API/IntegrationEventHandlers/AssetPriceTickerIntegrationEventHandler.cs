using Assets.API.Abstraction;
using Assets.API.Entites;
using EventBus.Abstraction;
using Services.Common;
using Services.Common.IntegrationEvents;

namespace Assets.API.IntegrationEventHandlers
{
    public class AssetPriceTickerIntegrationEventHandler : IIntegrationEventHandler<AssetPriceTickerIntegrationEvent>
    {
        private readonly ICacheService _cacheService;

        public AssetPriceTickerIntegrationEventHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Handle(AssetPriceTickerIntegrationEvent integrationEvent)
        {
            if (integrationEvent == null || !integrationEvent.IsValid())
                return;

            if (integrationEvent.QuoteAsset != CommonConstants.USDT)
                return;

            var asset = _cacheService.Asset.GetByKeys(integrationEvent.AssetType, integrationEvent.BaseAsset);
            if (asset == null)
            {
                asset = new AssetEntity(integrationEvent.AssetType, integrationEvent.BaseAsset);
                asset.PriceUSDT = integrationEvent.Price;
            }

            asset.PriceUSDT = integrationEvent.Price;

            await _cacheService.Asset.UpdateAsync(asset);
        }
    }
}