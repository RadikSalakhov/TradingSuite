using TaApi.BackgroundTasks.Settings;
using TaApi.BackgroundTasks.Structs;

namespace TaApi.BackgroundTasks.Entities
{
    public class BulkResponseIdEntity : BaseEntity
    {
        public string Exchange { get; set; } = string.Empty;

        public Asset Asset { get; set; }

        public TAInterval Interval { get; set; }

        public TAIndicator Indicator { get; set; }

        public decimal IndicatorValue { get; set; }

        public bool IsEmaValidShort()
        {
            if (!isEmaValid())
                return false;

            return IndicatorValue == ConstSettings.EMA_SHORT_PERIOD;
        }

        public bool IsEmaValidLong()
        {
            if (!isEmaValid())
                return false;

            return IndicatorValue == ConstSettings.EMA_LONG_PERIOD;
        }

        private bool isEmaValid()
        {
            if (Exchange != ConstSettings.TA_API_EXCAHNGE)
                return false;

            if (Interval == TAInterval._EMPTY)
                return false;

            if (Indicator == TAIndicator._EMPTY)
                return false;

            return true;
        }
    }
}