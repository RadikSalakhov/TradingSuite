using BlazorApp.Client.Abstraction;
using BlazorApp.Client.Entities;
using BlazorApp.Client.Services.ClientCacheEntries.Base;

namespace BlazorApp.Client.Services.ClientCacheEntries
{
    public class AssetCacheEntry : BaseDoubleDictCacheEntry<string, string, AssetEntity>
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly HashSet<string> _initializedAssets = new HashSet<string>();

        public AssetCacheEntry(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public AssetEntity? GetAssetWithInitialization(string assetType, string baseAsset)
        {
            if (string.IsNullOrWhiteSpace(assetType) || string.IsNullOrWhiteSpace(baseAsset))
                return null;

            checkInitialization(assetType, baseAsset);

            return GetByKeys(assetType, baseAsset);
        }

        public async Task UpdateAssetPriceAsync(string assetType, string baseAsset, string quoteAsset, decimal price)
        {
            if (string.IsNullOrWhiteSpace(assetType) || string.IsNullOrWhiteSpace(baseAsset))
                return;

            var assetEntity = GetByKeys(assetType, baseAsset);
            if (assetEntity == null)
                assetEntity = new AssetEntity(assetType, baseAsset);

            if (quoteAsset == "USDT")
            {
                assetEntity.PriceUSDT = price;
                await UpdateAsync(assetEntity);
            }
        }

        public async Task UpdateEmaCrossAsync(EmaCrossEntity emaCrossEntity)
        {
            if (emaCrossEntity == null)
                return;

            var assetEntity = GetByKeys(emaCrossEntity.AssetType, emaCrossEntity.BaseAsset);
            if (assetEntity == null)
                assetEntity = new AssetEntity(emaCrossEntity.AssetType, emaCrossEntity.BaseAsset);

            if (assetEntity.UpdateEmaCross(emaCrossEntity))
                await UpdateAsync(assetEntity);
        }

        protected override string GetKeyA(AssetEntity value)
        {
            return value.AssetType;
        }

        protected override string GetKeyB(AssetEntity value)
        {
            return value.BaseAsset;
        }

        private void checkInitialization(string assetType, string baseAsset)
        {
            var assetHash = $"{assetType}:{baseAsset}";
            if (_initializedAssets.Contains(assetHash))
                return;

            Task.Factory.StartNew(async () =>
            {
                using var scope = _serviceProvider.CreateScope();

                var assetsClientService = scope.ServiceProvider.GetRequiredService<IAssetsClientService>();

                var assetPriceDTO = await assetsClientService.GetAssetPriceAsync(assetType, baseAsset);
                if (assetPriceDTO != null)
                    await UpdateAssetPriceAsync(assetPriceDTO.AssetType, assetPriceDTO.BaseAsset, assetPriceDTO.QuoteAsset, assetPriceDTO.Price);

                var emaCrossDTOs = await assetsClientService.GetEmaCrossAsync(assetType, baseAsset);
                if (emaCrossDTOs != null)
                {
                    foreach (var emaCrossDTO in emaCrossDTOs)
                        await UpdateEmaCrossAsync(emaCrossDTO.ToEntity());
                }
            }).ConfigureAwait(false);

            _initializedAssets.Add(assetHash);
        }
    }
}