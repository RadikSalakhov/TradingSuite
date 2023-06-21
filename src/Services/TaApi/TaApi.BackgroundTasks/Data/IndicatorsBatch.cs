namespace TaApi.BackgroundTasks.Data
{
    public struct IndicatorsBatch
    {
        public TAIndicator TAIndicator { get; }

        public TAInterval TAInterval { get; }

        public IEnumerable<Asset> Assets { get; }

        public IndicatorsBatch()
        {
            TAIndicator = TAIndicator._EMPTY;
            TAInterval = TAInterval._EMPTY;
            Assets = new Asset[0];
        }

        public IndicatorsBatch(TAIndicator taIndicator, TAInterval taInterval, IEnumerable<Asset> assets)
        {
            TAIndicator = taIndicator;
            TAInterval = taInterval;
            Assets = assets ?? new Asset[0];
        }

        public bool IsEmpty()
        {
            return TAIndicator == TAIndicator._EMPTY || TAInterval == TAInterval._EMPTY || !Assets.Any();
        }
    }
}