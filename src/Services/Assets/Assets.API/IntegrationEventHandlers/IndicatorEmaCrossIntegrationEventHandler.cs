using Assets.Application.Aggregates;
using Assets.Application.Contracts;
using EventBus.Abstraction;
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

            var assetAggregate = _cacheService.AssetAggregate.GetByKeys(integrationEvent.AssetType, integrationEvent.BaseAsset);
            if (assetAggregate == null)
                assetAggregate = AssetAggregate.Create(integrationEvent.AssetType, integrationEvent.BaseAsset);

            assetAggregate.UpdateEmaCross(integrationEvent.Interval, integrationEvent.ValueShort, integrationEvent.ValueLong, integrationEvent.PrevValueShort, integrationEvent.PrevValueLong);

            await _cacheService.AssetAggregate.UpdateAsync(assetAggregate);
        }
    }
}