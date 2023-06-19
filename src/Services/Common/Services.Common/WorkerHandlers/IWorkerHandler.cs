namespace Services.Common.WorkerHandlers
{
    public interface IWorkerHandler
    {
        void RegisterAction<T>(long intervalMS, Func<Task> action);

        void UnregisteredActions<T>();

        Task ProcessStep();
    }
}