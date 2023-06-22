using Binance.Domain.Common;
using Binance.Domain.Entities;
using Binance.Domain.Services;

namespace Binance.Infrastructure.Services
{
    public class TechnicalIndicatorsService : ITechnicalIndicatorsService
    {
        private const int EMA_HISTORY_SECONDS = 1 * 60 * 60;

        private const int EMA_SHORT_PERIOD = 50;
        private const int EMA_LONG_PERIOD = 200;

        private readonly Dictionary<CryptoAsset, List<PriceTickerEntity>> _priceTickersBuffer = new();

        private readonly Dictionary<CryptoAsset, SortedDictionary<DateTime, decimal>> _pricesCache = new();

        private readonly Dictionary<CryptoAsset, List<EmaEntity>> _emaShort = new();
        private readonly Dictionary<CryptoAsset, List<EmaEntity>> _emaLong = new();

        private readonly Dictionary<CryptoAsset, List<EmaCrossEntity>> _emaCross = new();

        public EmaCrossEntity? GetEmaCrossEntity(CryptoAsset cryptoAsset)
        {
            processEMACross(cryptoAsset);

            List<EmaCrossEntity>? emaCrossList;
            lock (_emaCross)
            {
                if (!_emaCross.TryGetValue(cryptoAsset, out emaCrossList))
                    return null;
            }

            lock (emaCrossList)
            {
                if (emaCrossList.Any())
                {
                    var lastEmaCross = emaCrossList.Last();

                    var prevEmaCross = emaCrossList.Count >= 2 ? emaCrossList[emaCrossList.Count - 2] : lastEmaCross;

                    lastEmaCross.PrevValueShort = prevEmaCross.ValueShort;
                    lastEmaCross.PrevValueLong = prevEmaCross.ValueLong;

                    return lastEmaCross;
                }
            }

            return null;
        }

        public IEnumerable<EmaCrossEntity> GetEmaCrossEntities()
        {
            processEMACross();

            var resultList = new List<EmaCrossEntity>();

            foreach (var cryptoAsset in CryptoAsset.GetAll(skipUSDT: true))
            {
                List<EmaCrossEntity>? emaCrossList;
                lock (_emaCross)
                {
                    if (!_emaCross.TryGetValue(cryptoAsset, out emaCrossList))
                        continue;
                }

                lock (emaCrossList)
                {
                    if (emaCrossList.Any())
                    {
                        var lastEmaCross = emaCrossList.Last();

                        var prevEmaCross = emaCrossList.Count >= 2 ? emaCrossList[emaCrossList.Count - 2] : lastEmaCross;

                        lastEmaCross.PrevValueShort = prevEmaCross.ValueShort;
                        lastEmaCross.PrevValueLong = prevEmaCross.ValueLong;

                        resultList.Add(lastEmaCross);
                    }
                }
            }

            return resultList;
        }

        public void AddPriceTickersToBuffer(IEnumerable<PriceTickerEntity> priceTickers)
        {
            lock (_priceTickersBuffer)
            {
                lock (priceTickers)
                {
                    foreach (var priceTicker in priceTickers)
                    {
                        if (!_priceTickersBuffer.TryGetValue(priceTicker.CryptoAsset, out List<PriceTickerEntity>? targetPriceTickers))
                        {
                            targetPriceTickers = new List<PriceTickerEntity>();
                            _priceTickersBuffer.Add(priceTicker.CryptoAsset, targetPriceTickers);
                        }

                        targetPriceTickers.Add(priceTicker);
                    }
                }
            }
        }

        public IEnumerable<CryptoAsset> ProcessPriceTickersBuffer()
        {
            var processedCryptoAssets = processPriceTickersBuffer();

            if (processedCryptoAssets.Any())
            {
                calculateEmaIndicators(_emaShort, EMA_SHORT_PERIOD);

                calculateEmaIndicators(_emaLong, EMA_LONG_PERIOD);

                processEMACross();
            }

            return processedCryptoAssets;
        }

        private IEnumerable<CryptoAsset> processPriceTickersBuffer()
        {
            var priceTickersDict = new Dictionary<CryptoAsset, List<PriceTickerEntity>>();

            //Copy from buffer
            lock (_priceTickersBuffer)
            {
                foreach (var kvp in _priceTickersBuffer)
                {
                    if (!kvp.Value.Any())
                        continue;

                    var priceTickersList = new List<PriceTickerEntity>();
                    priceTickersDict.Add(kvp.Key, priceTickersList);

                    foreach (var priceTicker in kvp.Value)
                        priceTickersList.Add(priceTicker);

                    kvp.Value.Clear();
                }
            }

            var processedCryptoAssets = new List<CryptoAsset>();

            lock (_pricesCache)
            {
                //Add new
                foreach (var kvp in priceTickersDict)
                {
                    if (!_pricesCache.TryGetValue(kvp.Key, out SortedDictionary<DateTime, decimal>? pricesDict))
                    {
                        pricesDict = new SortedDictionary<DateTime, decimal>();
                        _pricesCache.Add(kvp.Key, pricesDict);
                    }

                    lock (pricesDict)
                    {
                        foreach (var priceTicker in kvp.Value)
                        {
                            var priceDT = truncateMillisecods(priceTicker.CreateDT);
                            if (pricesDict.ContainsKey(priceDT))
                                pricesDict[priceDT] = 0.5m * (pricesDict[priceDT] + priceTicker.Price);
                            else
                                pricesDict.Add(priceDT, priceTicker.Price);

                            processedCryptoAssets.Add(priceTicker.CryptoAsset);
                        }

                        //Remove old
                        var refDT = DateTime.UtcNow;
                        var keysToRemove = new List<DateTime>();
                        foreach (var key in pricesDict.Keys)
                        {
                            if ((refDT - key).TotalSeconds > EMA_HISTORY_SECONDS)
                                keysToRemove.Add(key);
                            else
                                break;
                        }

                        foreach (var key in keysToRemove)
                        {
                            if (pricesDict.ContainsKey(key))
                                pricesDict.Remove(key);
                        }
                    }
                }
            }

            return processedCryptoAssets;
        }

        private void calculateEmaIndicators(Dictionary<CryptoAsset, List<EmaEntity>> targetDict, int emaPeriod)
        {
            var multiplier = 2m / (emaPeriod + 1m);

            lock (_pricesCache)
            {
                foreach (var kvp in _pricesCache)
                {
                    //if (kvp.Key != CryptoAsset.BTC)
                    //    continue;

                    var sourceList = new List<EmaEntity>();

                    var pricesDict = kvp.Value;
                    lock (pricesDict)
                    {
                        var prevEmaValue = 0m;
                        var pricesBuffer = new List<decimal>();
                        foreach (var kvpPrice in pricesDict)
                        {
                            pricesBuffer.Add(kvpPrice.Value);
                            if (pricesBuffer.Count > emaPeriod)
                                pricesBuffer.RemoveAt(0);

                            var sourcePrice = pricesBuffer.Average();

                            if (prevEmaValue == 0m)
                                prevEmaValue = sourcePrice;

                            var emaValue = sourcePrice * multiplier + prevEmaValue * (1m - multiplier);

                            var emaEntity = BaseEntity.CreateNew<EmaEntity>();

                            emaEntity.ReferenceDT = kvpPrice.Key;
                            emaEntity.Value = emaValue;

                            sourceList.Add(emaEntity);

                            prevEmaValue = emaValue;
                        }
                    }

                    List<EmaEntity>? targetList;
                    lock (targetDict)
                    {
                        if (!targetDict.TryGetValue(kvp.Key, out targetList))
                        {
                            targetList = new List<EmaEntity>();
                            targetDict.Add(kvp.Key, targetList);
                        }
                    }

                    lock (targetList)
                    {
                        targetList.Clear();
                        if (sourceList.Any())
                            targetList.AddRange(sourceList);
                    }
                }
            }
        }

        private void processEMACross()
        {
            foreach (var cryptoAsset in CryptoAsset.GetAll(skipUSDT: true))
            {
                processEMACross(cryptoAsset);
            }
        }

        private void processEMACross(CryptoAsset cryptoAsset)
        {
            List<EmaCrossEntity>? emaCrossList;
            lock (_emaCross)
            {
                if (!_emaCross.TryGetValue(cryptoAsset, out emaCrossList))
                {
                    emaCrossList = new List<EmaCrossEntity>();
                    _emaCross.Add(cryptoAsset, emaCrossList);
                }
            }

            lock (emaCrossList)
            {
                emaCrossList.Clear();

                var emaLists = getEMALists(cryptoAsset);

                var emaShortList = emaLists?.Item1;
                var emaLongList = emaLists?.Item2;
                if (emaShortList == null || emaLongList == null)
                    return;

                lock (emaShortList)
                {
                    lock (emaLongList)
                    {
                        foreach (var emaShort in emaShortList)
                        {
                            var emaLong = emaLongList.Where(v => v.ReferenceDT == emaShort.ReferenceDT).FirstOrDefault();
                            if (emaLong == null)
                                continue;

                            var emaCross = BaseEntity.CreateNew<EmaCrossEntity>();
                            emaCross.ReferenceDT = emaShort.ReferenceDT;
                            emaCross.CryptoAsset = cryptoAsset;
                            emaCross.TAInterval = "1s";
                            emaCross.ValueShort = emaShort.Value;
                            emaCross.ValueLong = emaLong.Value;

                            emaCrossList.Add(emaCross);
                        }
                    }
                }
            }
        }

        private Tuple<List<EmaEntity>, List<EmaEntity>>? getEMALists(CryptoAsset cryptoAsset)
        {
            List<EmaEntity>? emaShortList;
            List<EmaEntity>? emaLongList;

            lock (_emaShort)
            {
                _emaShort.TryGetValue(cryptoAsset, out emaShortList);
            }

            lock (_emaLong)
            {
                _emaLong.TryGetValue(cryptoAsset, out emaLongList);
            }

            if (emaShortList == null || emaLongList == null)
                return null;

            return new Tuple<List<EmaEntity>, List<EmaEntity>>(emaShortList, emaLongList);
        }

        private static DateTime truncateMillisecods(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Kind);
        }
    }
}