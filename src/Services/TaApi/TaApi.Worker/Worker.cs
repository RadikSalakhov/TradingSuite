using EventBus.Abstraction;
using Microsoft.Extensions.Options;
using Services.Common.IntegrationEvents;
using Services.Common.WorkerHandlers;
using TaApi.Worker.Abstraction;
using TaApi.Worker.Configuration;
using TaApi.Worker.Data;

namespace TaApi.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IWorkerHandler _workerHandler;

        private readonly IEventBus _eventBus;

        private readonly TaApiOptions _taApiOptions;

        private readonly ITaApiService _taApiService;

        private readonly IEmaProcessor _emaProcessor;

        public Worker(ILogger<Worker> logger, IWorkerHandler workerHandler, IEventBus eventBus,
            IOptions<TaApiOptions> taApiOptions, ITaApiService taApiService, IEmaProcessor emaProcessor)
        {
            _logger = logger;
            _workerHandler = workerHandler;
            _eventBus = eventBus;
            _taApiOptions = taApiOptions.Value;
            _taApiService = taApiService;
            _emaProcessor = emaProcessor;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await _taApiService.InitializeAsync();

            var periodMS = _taApiOptions?.TaApiPeriodMS ?? 0;
            if (periodMS > 0)
                _workerHandler.RegisterAction<Worker>(periodMS, onEveryPeriod);

            await base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _workerHandler.UnregisteredActions<Worker>();

            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _workerHandler.ProcessStepAsync();
            }
        }

        private async Task onEveryPeriod()
        {
            var indicatorsBatch = await _taApiService.ProcessStepAsync();

            if (!indicatorsBatch.IsEmpty())
            {
                if (indicatorsBatch.TAIndicator == TAIndicator.EMA)
                {
                    foreach (var asset in indicatorsBatch.Assets)
                    {
                        var emaCross = _emaProcessor.GetEmaCrossEntity(asset, indicatorsBatch.TAInterval);
                        if (emaCross != null)
                        {
                            var integrationEvent = new IndicatorEmaCrossIntegrationEvent(emaCross.Asset, emaCross.TAInterval, emaCross.ValueShort, emaCross.ValueLong, emaCross.PrevValueShort, emaCross.PrevValueLong);
                            _eventBus.Publish(integrationEvent);
                        }
                    }
                }
            }
        }
    }
}