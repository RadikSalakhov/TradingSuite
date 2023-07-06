using TaApi.Worker.Data;

namespace TaApi.Worker.Abstraction
{
    public interface ITaApiService
    {
        Task InitializeAsync();

        Task<IndicatorsBatch> ProcessStepAsync();
    }
}