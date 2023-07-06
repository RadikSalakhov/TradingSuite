using Microsoft.Extensions.Options;
using TaApi.Worker.Abstraction;
using TaApi.Worker.Configuration;
using TaApi.Worker.DTO;
using TaApi.Worker.Entities;
using TaApi.Worker.Settings;
using TaApi.Worker.Data;
using Services.Common.Extensions;

namespace TaApi.Worker.Services
{
    public class TaApiService : ITaApiService
    {
        private const int MAX_AMOUNT = 240;

        private readonly ILogger _logger;

        private readonly TaApiOptions _taApiOptions;

        private readonly ITaApiClient _taApiClient;

        private readonly IEmaProcessor _emaProcessor;

        private readonly Dictionary<TAInterval, List<Func<Task<IndicatorsBatch>>>> _actionsDict = new();
        private readonly List<Func<Task<IndicatorsBatch>>> _actionsToProcess = new();

        private bool _backTracksInitialized;

        public TaApiService(IOptions<TaApiOptions> taApiOptions, ITaApiClient taApiClient, IEmaProcessor emaProcessor)
        {
            _taApiOptions = taApiOptions.Value;
            _taApiClient = taApiClient;
            _emaProcessor = emaProcessor;

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            _logger = loggerFactory.CreateLogger<TaApiService>();
        }

        public Task InitializeAsync()
        {
            initializeEmaIndicatorsActions();

            _logger.LogInformation($"TaApi | PeriodMS:{_taApiOptions.TaApiPeriodMS} | SymbolsPerRequest:{_taApiOptions.TaApiSymbolsPerRequest}");

            return Task.CompletedTask;
        }

        public async Task<IndicatorsBatch> ProcessStepAsync()
        {
            if (!_actionsToProcess.Any())
            {
                if (!_backTracksInitialized)
                {
                    _actionsToProcess.AddRange(getBacktrackActionsToProcess());
                    _backTracksInitialized = true;
                }
                else
                    _actionsToProcess.AddRange(getActionsToProcess());
            }

            if (_actionsToProcess.Any())
            {
                var actionToProcess = _actionsToProcess.ElementAt(0);

                var result = await actionToProcess();

                _actionsToProcess.RemoveAt(0);

                return result;
            }

            return new IndicatorsBatch();
        }

        private void initializeEmaIndicatorsActions()
        {
            var indicators = new BulkRequestIndicatorDTO[]
            {
                new BulkRequestIndicatorDTO(TAIndicator.EMA, ConstSettings.EMA_SHORT_PERIOD),
                new BulkRequestIndicatorDTO(TAIndicator.EMA, ConstSettings.EMA_LONG_PERIOD)
            };

            var batchSize = _taApiOptions.TaApiSymbolsPerRequest > 1 ? _taApiOptions.TaApiSymbolsPerRequest : 1;

            foreach (var taInterval in _emaProcessor.GetSupportedIntervals())
            {
                if (!_actionsDict.TryGetValue(taInterval, out List<Func<Task<IndicatorsBatch>>>? actionsList))
                {
                    actionsList = new List<Func<Task<IndicatorsBatch>>>();
                    _actionsDict.Add(taInterval, actionsList);
                }

                var assetsBatches = _emaProcessor.GetSupportedAssets().Batch(batchSize);
                foreach (var assetsBatch in assetsBatches)
                {
                    var assetsBatchList = assetsBatch.ToList();
                    actionsList.Add(async () => await processBulkIndicators<EmaEntity, EmaDTO>(new IndicatorsBatch(TAIndicator.EMA, taInterval, assetsBatchList), indicators, _emaProcessor.GetTargetEmaList));
                }
            }
        }

        private IEnumerable<Func<Task<IndicatorsBatch>>> getBacktrackActionsToProcess()
        {
            var backtracks = _taApiOptions.TaApiSymbolsPerRequest > 1 ? _taApiOptions.TaApiSymbolsPerRequest : 1;

            var indicators = new BulkRequestIndicatorDTO[]
            {
                    new BulkRequestIndicatorDTO(TAIndicator.EMA, ConstSettings.EMA_SHORT_PERIOD, backtracks),
                    new BulkRequestIndicatorDTO(TAIndicator.EMA, ConstSettings.EMA_LONG_PERIOD, backtracks)
            };

            foreach (var taInterval in _emaProcessor.GetSupportedIntervals())
            {
                foreach (var asset in _emaProcessor.GetSupportedAssets())
                    yield return async () => await processBulkIndicators<EmaEntity, EmaDTO>(new IndicatorsBatch(TAIndicator.EMA, taInterval, new[] { asset }), indicators, _emaProcessor.GetTargetEmaList);
            }
        }

        private IEnumerable<Func<Task<IndicatorsBatch>>> getActionsToProcess()
        {
            foreach (var action in getActionsToProcess(TAInterval.Interval_1m))
                yield return action;

            foreach (var action in getActionsToProcess(TAInterval.Interval_5m))
                yield return action;

            foreach (var action in getActionsToProcess(TAInterval.Interval_1m))
                yield return action;

            foreach (var action in getActionsToProcess(TAInterval.Interval_15m))
                yield return action;

            foreach (var action in getActionsToProcess(TAInterval.Interval_1m))
                yield return action;

            foreach (var action in getActionsToProcess(TAInterval.Interval_5m))
                yield return action;

            foreach (var action in getActionsToProcess(TAInterval.Interval_1m))
                yield return action;

            foreach (var action in getActionsToProcess(TAInterval.Interval_30m))
                yield return action;

            foreach (var action in getActionsToProcess(TAInterval.Interval_1m))
                yield return action;

            foreach (var action in getActionsToProcess(TAInterval.Interval_5m))
                yield return action;

            foreach (var action in getActionsToProcess(TAInterval.Interval_1m))
                yield return action;

            foreach (var action in getActionsToProcess(TAInterval.Interval_1h))
                yield return action;
        }

        private IEnumerable<Func<Task<IndicatorsBatch>>> getActionsToProcess(TAInterval taInterval)
        {
            if (_actionsDict.TryGetValue(taInterval, out List<Func<Task<IndicatorsBatch>>>? actionsList))
            {
                foreach (var action in actionsList)
                    yield return action;
            }
        }

        private async Task<bool> processIndicators<TEntity, TDTO>(List<TEntity> targetEntites, string symbol, TAInterval taInterval, TAIndicator taIndicator, string extraParameters)
            where TEntity : BaseEntity, new()
            where TDTO : BaseDTO<TEntity>
        {
            var processed = false;
            if (!targetEntites.Any())
            {
                var entities = await _taApiClient.GetEntities<TEntity, TDTO>(symbol, taInterval, taIndicator, extraParameters);
                if (entities.Any())
                {
                    lock (targetEntites)
                    {
                        if (!targetEntites.Any())
                        {
                            targetEntites.AddRange(entities.OrderBy(v => v.ReferenceDT));
                            processed = true;
                        }
                    }
                }
            }
            else
            {
                var entity = await _taApiClient.GetEntity<TEntity, TDTO>(symbol, taInterval, taIndicator, extraParameters);
                if (entity != null)
                {
                    lock (targetEntites)
                    {
                        if (targetEntites.Any())
                        {
                            var lastEntity = targetEntites.Last();
                            if (entity.ReferenceDT > lastEntity.ReferenceDT)
                            {
                                targetEntites.Add(entity);
                                processed = true;
                            }
                            else if (entity.ReferenceDT == lastEntity.ReferenceDT)
                            {
                                targetEntites.RemoveAt(targetEntites.Count - 1);
                                targetEntites.Add(entity);
                                processed = true;
                            }
                        }
                        else
                        {
                            targetEntites.Add(entity);
                            processed = true;
                        }
                    }
                }
            }

            if (targetEntites.Count > MAX_AMOUNT)
            {
                lock (targetEntites)
                {
                    if (targetEntites.Count > MAX_AMOUNT)
                    {
                        targetEntites.RemoveAt(0);
                        processed = true;
                    }
                }
            }

            return processed;
        }

        private async Task<IndicatorsBatch> processBulkIndicators<TEntity, TDTO>(IndicatorsBatch indicatorsBatch, IEnumerable<BulkRequestIndicatorDTO> indicators, Func<BulkResponseIdEntity, IList<TEntity>?> getTargetListFunc)
            where TEntity : BaseEntity, new()
            where TDTO : BaseDTO<TEntity>
        {
            if (!indicatorsBatch.Assets.Any())
                return indicatorsBatch;

            if (indicators == null || !indicators.Any())
                return indicatorsBatch;

            var symbols = indicatorsBatch.Assets.Select(v => $"{v.Value}/USDT");

            var tuples = await _taApiClient.GetBulkEntities<TEntity, TDTO>(symbols, indicators, indicatorsBatch.TAInterval);
            if (tuples == null || !tuples.Any())
                return indicatorsBatch;

            foreach (var tuple in tuples)
            {
                var id = tuple.Item1;
                var entity = tuple.Item2;

                if (id == null || entity == null)
                    continue;

                var targetList = getTargetListFunc(id);
                if (targetList == null)
                    continue;

                lock (targetList)
                {
                    if (targetList.Any())
                    {
                        var lastEntity = targetList.Last();
                        if (entity.ReferenceDT > lastEntity.ReferenceDT)
                        {
                            targetList.Add(entity);
                        }
                        else if (entity.ReferenceDT < lastEntity.ReferenceDT)
                        {
                            targetList.Add(entity);
                            targetList.SortBy(v => v.ReferenceDT);
                        }
                        else if (entity.ReferenceDT == lastEntity.ReferenceDT)
                        {
                            targetList.RemoveAt(targetList.Count - 1);
                            targetList.Add(entity);
                        }

                        if (targetList.Count > MAX_AMOUNT)
                            targetList.RemoveAt(0);
                    }
                    else
                    {
                        targetList.Add(entity);
                    }
                }
            }

            return indicatorsBatch;
        }
    }
}