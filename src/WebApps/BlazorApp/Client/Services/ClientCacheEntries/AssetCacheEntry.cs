using BlazorApp.Client.Entities;
using BlazorApp.Client.Services.ClientCacheEntries.Base;

namespace BlazorApp.Client.Services.ClientCacheEntries
{
    public class AssetCacheEntry : BaseDoubleDictCacheEntry<string, string, AssetEntity>
    {
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

        public async Task UpdateEmaCross(EmaCrossEntity emaCrossEntity)
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
    }
}