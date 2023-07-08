using Assets.API.Abstraction;
using Assets.API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Assets.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        private readonly IBinanceWorkerAPIService _binanceWorkerAPIService;

        public AssetsController(ICacheService cacheService, IBinanceWorkerAPIService binanceWorkerAPIService)
        {
            _cacheService = cacheService;
            _binanceWorkerAPIService = binanceWorkerAPIService;
        }

        [HttpGet("get-asset-price")]
        public async Task<ActionResult<AssetPriceDTO>> GetAssetPrice(string assetType, string baseAsset)
        {
            var assetEntity = _cacheService.Asset.GetByKeys(assetType, baseAsset);
            if (assetEntity == null)
                return NotFound();

            var dto = AssetPriceDTO.FromEntity(assetEntity);

            return Ok(dto);
        }

        [HttpGet("get-ema-cross")]
        public async Task<ActionResult<IEnumerable<EmaCrossDTO>>> GetEmaCross(string assetType, string baseAsset)
        {
            var assetEntity = _cacheService.Asset.GetByKeys(assetType, baseAsset);
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
        public async Task<ActionResult<IEnumerable<string>>> GetTest()
        {
            var result = await _binanceWorkerAPIService.GetBinanceAssets();

            return Ok(result);
        }
    }
}