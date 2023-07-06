using TaApi.Worker.DTO;
using TaApi.Worker.Entities;
using TaApi.Worker.Data;

namespace TaApi.Worker.Abstraction
{
    public interface ITaApiClient
    {
        Task<TEntity?> GetEntity<TEntity, TDTO>(string symbol, TAInterval taInterval, TAIndicator taIndicator, string extraParameters)
            where TEntity : BaseEntity, new()
            where TDTO : BaseDTO<TEntity>;

        Task<IEnumerable<TEntity>> GetEntities<TEntity, TDTO>(string symbol, TAInterval taInterval, TAIndicator taIndicator, string extraParameters)
            where TEntity : BaseEntity, new()
            where TDTO : BaseDTO<TEntity>;

        Task<IEnumerable<Tuple<BulkResponseIdEntity, TEntity>>> GetBulkEntities<TEntity, TDTO>(IEnumerable<string> symbols, IEnumerable<BulkRequestIndicatorDTO> indicators, TAInterval taInterval)
            where TEntity : BaseEntity, new()
            where TDTO : BaseDTO<TEntity>;
    }
}