using TaApi.BackgroundTasks.Data;

namespace TaApi.BackgroundTasks.Abstraction
{
    public interface ITaApiService
    {
        Task InitializeAsync();

        Task<IndicatorsBatch> ProcessStepAsync();
    }
}