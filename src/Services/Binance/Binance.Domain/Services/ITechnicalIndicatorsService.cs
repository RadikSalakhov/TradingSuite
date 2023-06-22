using Binance.Domain.Common;
using Binance.Domain.Entities;

namespace Binance.Domain.Services
{
    public interface ITechnicalIndicatorsService
    {
        EmaCrossEntity? GetEmaCrossEntity(CryptoAsset cryptoAsset);

        IEnumerable<EmaCrossEntity> GetEmaCrossEntities();

        void AddPriceTickersToBuffer(IEnumerable<PriceTickerEntity> priceTickers);

        IEnumerable<CryptoAsset> ProcessPriceTickersBuffer();
    }
}