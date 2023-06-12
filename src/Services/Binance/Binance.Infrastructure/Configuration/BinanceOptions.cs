using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Infrastructure.Configuration
{
    public sealed class BinanceOptions
    {
        public string BinanceApiKey { get; set; } = string.Empty;

        public string BinanceApiSecret { get; set; } = string.Empty;
    }
}