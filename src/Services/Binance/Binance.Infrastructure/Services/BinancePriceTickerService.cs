using Binance.Common;
using Binance.Domain.Common;
using Binance.Domain.Entities;
using Binance.Domain.Services;
using Binance.Infrastructure.Configuration;
using Binance.Infrastructure.DTO;
using Binance.Spot;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Infrastructure.Services
{
    public class BinancePriceTickerService : IBinancePriceTickerService
    {
        private const string BINANCE_BASE_URL = "https://api.binance.com";

        private readonly ILogger _logger;

        private string _apiKey = string.Empty;

        private string _apiSecret = string.Empty;

        public BinancePriceTickerService(IOptions<BinanceOptions> options)
        {
            _apiKey = options.Value.BinanceApiKey;
            _apiSecret = options.Value.BinanceApiSecret;

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            _logger = loggerFactory.CreateLogger<BinancePriceTickerService>();
        }

        public async Task<ServerTimeEntity> GetServerTime()
        {
            try
            {
                throwIfNotRegistered();

                using var httpClient = getHttpClient();

                var resultJson = await getMarket(httpClient).CheckServerTime();

                var dto = JsonConvert.DeserializeObject<BinanceServerTimeDTO>(resultJson) ?? new BinanceServerTimeDTO();

                return dto.GetEntity();
            }
            catch (BinanceClientException exp)
            {
                logBinanceClientException(exp);
                throw;
            }
        }

        public async Task<IEnumerable<PriceTickerEntity>> GetPriceTickers(IEnumerable<CryptoAsset> cryptoAssets)
        {
            var cryptoSymbols = cryptoAssets?.Select(v => CryptoSymbol.CreateFromCryptoAsset(v)) ?? new List<CryptoSymbol>();
            return await GetPriceTickers(cryptoSymbols);
        }

        public async Task<IEnumerable<PriceTickerEntity>> GetPriceTickers(IEnumerable<CryptoSymbol> cryptoSymbols)
        {
            try
            {
                throwIfNotRegistered();

                using var httpClient = getHttpClient();

                var symbolsStr = string.Join(",", cryptoSymbols.Select(v => $"\"{v}\""));

                var resultJson = await getMarket(httpClient).SymbolPriceTicker(symbols: $"[{symbolsStr}]");

                var dtoArray = JsonConvert.DeserializeObject<BinancePriceTickerDTO[]>(resultJson) ?? new BinancePriceTickerDTO[0];

                return dtoArray.Select(v => v.GetEntity()).ToArray();
            }
            catch (BinanceClientException exp)
            {
                logBinanceClientException(exp);
                throw;
            }
        }

        private HttpClient getHttpClient()
        {
            var loggingHandler = new BinanceLoggingHandler(logger: _logger);
            return new HttpClient(handler: loggingHandler);
        }

        private Market getMarket(HttpClient httpClient)
        {
            return new Market(httpClient, baseUrl: BINANCE_BASE_URL, apiKey: _apiKey, apiSecret: _apiSecret);
        }

        private void throwIfNotRegistered()
        {
            if (string.IsNullOrWhiteSpace(_apiKey) || string.IsNullOrWhiteSpace(_apiSecret))
                throw new Exception("'BinanceManager' should be registered before it can be used");
        }

        private void logBinanceClientException(BinanceClientException binanceClientException)
        {
            _logger.LogError($"BinanceClientException: {binanceClientException.Code}; {binanceClientException.Message}");
        }
    }
}