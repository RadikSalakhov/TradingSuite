using System.Drawing;

namespace Binance.Domain
{
    public static class GeneralConstants
    {
        public const int MAX_TIMEOUT_MS = 7000;

        public const int WAIT_TIMEOUT_AFTER_TRADING = 3000;

        public const decimal ASSET_MIN_USDT_AMOUNT = 1m;

        public const decimal ASSET_MIN_SELLABLE_USDT_AMOUNT = 25m;

        public const decimal MIN_PNL_COEFF = 0.001m;

        public const decimal ASSET_FEE_COEFF = 0.075m / 100m;

        public const string TA_API_EXCAHNGE = "binance";

        public static Color ColorPlus = Color.DarkGreen;// Color.SeaGreen;

        public static Color ColorMinus = Color.DarkRed;// Color.IndianRed;
    }
}