﻿using Assets.Domain.Base;

namespace Assets.Domain.Keys
{
    public class AssetKey : BaseKey
    {
        public string AssetType { get; private set; }

        public string BaseAsset { get; private set; }

        public AssetKey(string assetType, string baseAsset)
        {
            AssetType = assetType;
            BaseAsset = baseAsset;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(AssetType)
                && !string.IsNullOrWhiteSpace(BaseAsset);
        }
    }
}