using Assets.Application.Contracts;
using Assets.Domain.Entites;
using Assets.WebApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Assets.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        private readonly IBinanceWorkerApiService _binanceWorkerAPIService;

        public AssetsController(ICacheService cacheService, IBinanceWorkerApiService binanceWorkerAPIService)
        {
            _cacheService = cacheService;
            _binanceWorkerAPIService = binanceWorkerAPIService;
        }

        [HttpGet("get-asset-price")]
        public async Task<ActionResult<AssetPriceDTO>> GetAssetPrice(string assetType, string baseAsset)
        {
            var assetAggregate = _cacheService.AssetAggregate.GetByKeys(assetType, baseAsset);
            if (assetAggregate == null)
                return NotFound();

            var dto = AssetPriceDTO.FromEntity(assetAggregate.AssetPriceEntity);

            return Ok(dto);
        }

        [HttpGet("get-ema-cross")]
        public async Task<ActionResult<IEnumerable<EmaCrossDTO>>> GetEmaCross(string assetType, string baseAsset)
        {
            var assetEntity = _cacheService.AssetAggregate.GetByKeys(assetType, baseAsset);
            if (assetEntity == null)
                return NotFound();

            var emaCrossEntities = assetEntity.GetEmaCrossEntities();
            if (!emaCrossEntities.Any())
                return NotFound();

            var dtos = new List<EmaCrossDTO>();

            foreach (var entity in emaCrossEntities)
                dtos.Add(EmaCrossDTO.FromEntity(entity));

            return Ok(dtos);
        }

        [HttpGet("get-test")]
        public async Task<ActionResult<IEnumerable<AssetEntity>>> GetTest()
        {
            var result = await _binanceWorkerAPIService.GetBinanceAssets();

            return Ok(result);
        }
    }
}