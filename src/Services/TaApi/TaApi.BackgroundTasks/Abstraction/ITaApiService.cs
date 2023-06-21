namespace TaApi.BackgroundTasks.Abstraction
{
    public interface ITaApiService
    {
        Task InitializeAsync();

        Task ProcessStepAsync();
    }
}