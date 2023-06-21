using Microsoft.Extensions.Logging;

namespace Services.Common.WorkerHandlers
{
    public class WorkerHandler : IWorkerHandler
    {
        private readonly Dictionary<Type, List<WorkerHandlerAction>> _registeredActionsDict = new();

        private readonly ILogger _logger;

        public DateTime WorkerDT { get; private set; } = DateTime.MinValue;

        public WorkerHandler()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            _logger = loggerFactory.CreateLogger<WorkerHandler>();
        }

        public void RegisterAction<T>(long intervalMS, Func<Task> action)
        {
            lock (_registeredActionsDict)
            {
                if (!_registeredActionsDict.TryGetValue(typeof(T), out List<WorkerHandlerAction>? registeredActions))
                {
                    registeredActions = new List<WorkerHandlerAction>();
                    _registeredActionsDict.Add(typeof(T), registeredActions);
                }

                registeredActions.Add(
                        new WorkerHandlerAction
                        {
                            IntervalMS = intervalMS,
                            Action = action,
                            LastDT = DateTime.MinValue
                        });
            }
        }

        public void UnregisteredActions<T>()
        {
            lock (_registeredActionsDict)
            {
                if (_registeredActionsDict.TryGetValue(typeof(T), out List<WorkerHandlerAction>? registeredActions))
                    registeredActions.Clear();
            }
        }

        public async Task ProcessStepAsync()
        {
            var executeActions = new List<WorkerHandlerAction>();

            WorkerDT = DateTime.UtcNow;

            lock (_registeredActionsDict)
            {
                foreach (var kvp in _registeredActionsDict)
                {
                    foreach (var registeredAction in kvp.Value)
                    {
                        if ((DateTime.UtcNow - registeredAction.LastDT).TotalMilliseconds >= registeredAction.IntervalMS)
                            executeActions.Add(registeredAction);
                    }
                }
            }

            if (executeActions.Any())
            {
                foreach (var executeAction in executeActions)
                {
                    try
                    {
                        await executeAction.Action();
                    }
                    catch (Exception exp)
                    {
                        _logger.LogError(exp, string.Empty);
                    }
                    finally
                    {
                        executeAction.LastDT = DateTime.UtcNow;
                    }
                }
            }
            else
            {
                await Task.Delay(1);
            }
        }
    }
}