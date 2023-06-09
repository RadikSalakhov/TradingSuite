﻿using EventBus.Events;

namespace Services.Common.IntegrationEvents
{
    public class IndicatorEmaCrossIntegrationEvent : IntegrationEvent
    {
        public string AssetType { get; }

        public string BaseAsset { get; }

        public string Interval { get; }

        public decimal ValueShort { get; }

        public decimal ValueLong { get; }

        public decimal PrevValueShort { get; }

        public decimal PrevValueLong { get; }

        public IndicatorEmaCrossIntegrationEvent(string assetType, string baseAsset, string interval, decimal valueShort, decimal valueLong, decimal prevValueShort, decimal prevValueLong)
        {
            AssetType = assetType;
            BaseAsset = baseAsset;
            Interval = interval;
            ValueShort = valueShort;
            ValueLong = valueLong;
            PrevValueShort = prevValueShort;
            PrevValueLong = prevValueLong;
        }

        public bool IsValid()
        {
            return
                !string.IsNullOrWhiteSpace(AssetType) &&
                !string.IsNullOrWhiteSpace(BaseAsset) &&
                !string.IsNullOrWhiteSpace(Interval);
        }
    }
}