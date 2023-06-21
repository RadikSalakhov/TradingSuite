using TaApi.BackgroundTasks.Entities;
using TaApi.BackgroundTasks.Data;

namespace TaApi.BackgroundTasks.Abstraction
{
    public interface IEmaProcessor
    {
        IList<EmaEntity>? GetTargetEmaList(BulkResponseIdEntity bulkResponseId);

        IEnumerable<Asset> GetSupportedAssets();

        IEnumerable<TAInterval> GetSupportedIntervals();

        EmaCrossEntity? GetEmaCrossEntity(Asset asset, TAInterval taInterval);

        IEnumerable<EmaCrossEntity> GetEmaCrossEntities();
    }
}