using OnlineStore.Application.Interfaces;

namespace OnlineStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly IProductServices _productServices;
        private readonly IFilterService _filterService;

        public FilterController(IProductServices productServices,
            IFilterService filterService)
        {
            _productServices = productServices;
            _filterService = filterService;
        }

        [HttpGet("FilterByPrice")]
        public IActionResult FilterByPrice( decimal minPrice, decimal maxPrice)
        {
            if (minPrice < 0 || maxPrice < 0 || minPrice > maxPrice)
            {
                return BadRequest("Invalid price range.");
            }

            var products = _filterService.FilterByPrice(minPrice, maxPrice);
            return Ok(products);
        }

        [HttpGet("FilterBySale")]
        public IActionResult FilterBySale()
        {
            var products = _filterService.FilterBySale();
            return Ok(products);
        }
    }
}
