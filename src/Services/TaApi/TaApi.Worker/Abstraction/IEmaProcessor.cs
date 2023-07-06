using TaApi.Worker.Entities;
using TaApi.Worker.Data;

namespace TaApi.Worker.Abstraction
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