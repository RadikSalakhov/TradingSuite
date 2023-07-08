using Binance.Domain.Common;
using Binance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Domain.Services
{
    public interface IBinancePriceTickerService
    {
        Task<ServerTimeEntity> GetServerTime();

        Task<IEnumerable<CryptoAsset>> GetAllSupportedCryptoAssets();

        Task<IEnumerable<CryptoAsset>> GetCurrentCryptoAssets();

        Task<IEnumerable<PriceTickerEntity>> GetPriceTickers(IEnumerable<CryptoAsset> cryptoAssets);

        Task<IEnumerable<PriceTickerEntity>> GetPriceTickers(IEnumerable<CryptoSymbol> cryptoSymbols);
    }
}