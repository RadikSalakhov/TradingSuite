using Microsoft.Extensions.Options;
using TaApi.BackgroundTasks.Abstraction;
using TaApi.BackgroundTasks.Configuration;
using TaApi.BackgroundTasks.Entities;
using TaApi.BackgroundTasks.Data;

namespace TaApi.BackgroundTasks.Services
{
    public class EmaProcessor : IEmaProcessor
    {
        private readonly Dictionary<Asset, Dictionary<TAInterval, List<EmaEntity>>> _emaShort = new();
        private readonly Dictionary<Asset, Dictionary<TAInterval, List<EmaEntity>>> _emaLong = new();

        private readonly Dictionary<Asset, Dictionary<TAInterval, List<EmaCrossEntity>>> _emaCross = new();

        private readonly IEnumerable<Asset> _assets = new Asset[]
        {
            "ADA", "APT", "ARB", "ATOM", "AVAX", "BNB", "BTC", "CAKE", "CFX", "DOGE",
            "DOT", "ETH", "FIL", "FTM", "ICP", "ID", "INJ", "LINK", "LTC", "MANA",
            "MATIC", "OP", "RNDR", "SAND", "SHIB", "SOL", "TRX", "UNI", "XLM", "XMR", "XRP"
        };

        private readonly IEnumerable<TAInterval> _taIntervals = new TAInterval[]
        {
            TAInterval.Interval_1m, TAInterval.Interval_5m, TAInterval.Interval_15m, TAInterval.Interval_30m, TAInterval.Interval_1h
        };

        public IEnumerable<Asset> GetSupportedAssets()
        {
            return _assets;
        }

        public IEnumerable<TAInterval> GetSupportedIntervals()
        {
            return _taIntervals;
        }

        public IList<EmaEntity>? GetTargetEmaList(BulkResponseIdEntity bulkResponseId)
        {
            if (bulkResponseId == null)
                return null;

            if (bulkResponseId.IsEmaValidShort())
                return getEMAListShort(bulkResponseId.Asset, bulkResponseId.Interval);

            if (bulkResponseId.IsEmaValidLong())
                return getEMAListLong(bulkResponseId.Asset, bulkResponseId.Interval);

            return null;
        }

        public EmaCrossEntity? GetEmaCrossEntity(Asset asset, TAInterval taInterval)
        {
            processEMACross(asset, taInterval);

            Dictionary<TAInterval, List<EmaCrossEntity>>? emaCrossDict;
            lock (_emaCross)
            {
                if (!_emaCross.TryGetValue(asset, out emaCrossDict))
                    return null;
            }

            List<EmaCrossEntity>? emaCrossList;
            lock (emaCrossDict)
            {
                if (!emaCrossDict.TryGetValue(taInterval, out emaCrossList))
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

            foreach (var asset in _assets)
            {
                Dictionary<TAInterval, List<EmaCrossEntity>>? emaCrossDict;
                lock (_emaCross)
                {
                    if (!_emaCross.TryGetValue(asset, out emaCrossDict))
                        continue;
                }

                foreach (var taInterval in _taIntervals)
                {
                    List<EmaCrossEntity>? emaCrossList;
                    lock (emaCrossDict)
                    {
                        if (!emaCrossDict.TryGetValue(taInterval, out emaCrossList))
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
            }

            return resultList;
        }

        private List<EmaEntity> getEMAListShort(Asset cryptoAsset, TAInterval taInterval)
        {
            return getEMAList(_emaShort, cryptoAsset, taInterval);
        }

        private List<EmaEntity> getEMAListLong(Asset cryptoAsset, TAInterval taInterval)
        {
            return getEMAList(_emaLong, cryptoAsset, taInterval);
        }

        private static List<EmaEntity> getEMAList(Dictionary<Asset, Dictionary<TAInterval, List<EmaEntity>>> cryptoAssetDict, Asset cryptoAsset, TAInterval taInterval)
        {
            Dictionary<TAInterval, List<EmaEntity>>? taIntervalDict;

            lock (cryptoAssetDict)
            {
                if (!cryptoAssetDict.TryGetValue(cryptoAsset, out taIntervalDict))
                {
                    taIntervalDict = new Dictionary<TAInterval, List<EmaEntity>>();
                    cryptoAssetDict.Add(cryptoAsset, taIntervalDict);
                }
            }

            List<EmaEntity>? emaList;
            lock (taIntervalDict)
            {
                if (!taIntervalDict.TryGetValue(taInterval, out emaList))
                {
                    emaList = new List<EmaEntity>();
                    taIntervalDict.Add(taInterval, emaList);
                }
            }

            return emaList;
        }

        private void processEMACross()
        {
            foreach (var asset in _assets)
            {
                foreach (var taInterval in _taIntervals)
                {
                    processEMACross(asset, taInterval);
                }
            }
        }

        private void processEMACross(Asset asset, TAInterval taInterval)
        {
            Dictionary<TAInterval, List<EmaCrossEntity>>? emaCrossDict;
            lock (_emaCross)
            {
                if (!_emaCross.TryGetValue(asset, out emaCrossDict))
                {
                    emaCrossDict = new Dictionary<TAInterval, List<EmaCrossEntity>>();
                    _emaCross.Add(asset, emaCrossDict);
                }
            }

            List<EmaCrossEntity>? emaCrossList;
            lock (emaCrossDict)
            {
                if (!emaCrossDict.TryGetValue(taInterval, out emaCrossList))
                {
                    emaCrossList = new List<EmaCrossEntity>();
                    emaCrossDict.Add(taInterval, emaCrossList);
                }
            }

            lock (emaCrossList)
            {
                emaCrossList.Clear();

                var emaListShort = getEMAListShort(asset, taInterval);
                var emaListLong = getEMAListLong(asset, taInterval);

                lock (emaListShort)
                {
                    lock (emaListLong)
                    {
                        foreach (var emaShort in emaListShort)
                        {
                            var emaLong = emaListLong.Where(v => v.ReferenceDT == emaShort.ReferenceDT).FirstOrDefault();
                            if (emaLong == null)
                                continue;

                            var emaCross = new EmaCrossEntity();
                            emaCross.ReferenceDT = emaShort.ReferenceDT;
                            emaCross.Asset = asset;
                            emaCross.TAInterval = taInterval;
                            emaCross.ValueShort = emaShort.Value;
                            emaCross.ValueLong = emaLong.Value;

                            emaCrossList.Add(emaCross);
                        }
                    }
                }
            }
        }
    }
}