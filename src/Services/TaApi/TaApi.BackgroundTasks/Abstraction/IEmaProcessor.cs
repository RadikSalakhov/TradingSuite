using TaApi.BackgroundTasks.Entities;
using TaApi.BackgroundTasks.Structs;

namespace TaApi.BackgroundTasks.Abstraction
{
    public interface IEmaProcessor
    {
        IList<EmaEntity>? GetTargetEmaList(BulkResponseIdEntity bulkResponseId);

        IEnumerable<Asset> GetSupportedAssets();

        IEnumerable<TAInterval> GetSupportedIntervals();
    }
}