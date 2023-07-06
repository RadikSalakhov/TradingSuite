using Assets.API.Abstraction;
using Assets.API.Entites;
using EventBus.Abstraction;
using Services.Common;
using Services.Common.IntegrationEvents;

namespace Assets.API.IntegrationEventHandlers
{
    public class IndicatorEmaCrossIntegrationEventHandler : IIntegrationEventHandler<IndicatorEmaCrossIntegrationEvent>
    {
        private readonly ICacheService _cacheService;

        public IndicatorEmaCrossIntegrationEventHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Handle(IndicatorEmaCrossIntegrationEvent integrationEvent)
        {
            if (integrationEvent == null || !integrationEvent.IsValid())
                return;

            var asset = _cacheService.Asset.GetByKeys(integrationEvent.AssetType, integrationEvent.BaseAsset);
            if (asset == null)
                asset = new AssetEntity(integrationEvent.AssetType, integrationEvent.BaseAsset);

            asset.UpdateEmaCross(integrationEvent.Interval, integrationEvent.ValueShort, integrationEvent.ValueLong, integrationEvent.PrevValueShort, integrationEvent.PrevValueLong);

            await _cacheService.Asset.UpdateAsync(asset);
        }
    }
}