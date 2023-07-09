using Assets.Application.Aggregates;
using Assets.Application.Contracts;
using EventBus.Abstraction;
using Services.Common;
using Services.Common.IntegrationEvents;

namespace Assets.Application.IntegrationEventHandlers
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

            var assetAggregate = _cacheService.AssetAggregate.GetByKeys(integrationEvent.AssetType, integrationEvent.BaseAsset);
            if (assetAggregate == null)
                assetAggregate = AssetAggregate.Create(integrationEvent.AssetType, integrationEvent.BaseAsset);

            assetAggregate.AssetPriceEntity.PriceUSDT = integrationEvent.Price;

            await _cacheService.AssetAggregate.UpdateAsync(assetAggregate);
        }
    }
}