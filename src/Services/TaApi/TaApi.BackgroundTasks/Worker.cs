using EventBus.Abstraction;
using Microsoft.Extensions.Options;
using Services.Common.WorkerHandlers;
using TaApi.BackgroundTasks.Abstraction;
using TaApi.BackgroundTasks.Configuration;

namespace TaApi.BackgroundTasks
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IWorkerHandler _workerHandler;

        private readonly IEventBus _eventBus;

        private readonly TaApiOptions _taApiOptions;

        private readonly ITaApiService _taApiService;

        public Worker(ILogger<Worker> logger, IWorkerHandler workerHandler, IEventBus eventBus, IOptions<TaApiOptions> taApiOptions, ITaApiService taApiService)
        {
            _logger = logger;
            _workerHandler = workerHandler;
            _eventBus = eventBus;
            _taApiOptions = taApiOptions.Value;
            _taApiService = taApiService;
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
            await _taApiService.ProcessStepAsync();
        }
    }
}