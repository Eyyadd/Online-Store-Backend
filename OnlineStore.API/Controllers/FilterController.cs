using OnlineStore.Application.DTOs;
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
        public IActionResult FilterByPrice(decimal minPrice, decimal maxPrice)
        {
            var response = new GeneralResponse<IEnumerable<ProductDTO>>(false, "No filteration result", []);
            var products = _filterService.FilterByPrice(minPrice, maxPrice);
            if (products.Any())
            {
                response.Success = true;
                response.Message = "Filteration By Price Successfully";
                response.Data = products;
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("FilterBySale")]
        public IActionResult FilterBySale()
        {
            var response = new GeneralResponse<IEnumerable<ProductDTO>>(false, "No filteration result", []);
            var products = _filterService.FilterBySale();
            if (products.Any())
            {
                response.Success = true;
                response.Message = "Filteration By Sale Successfully";
                response.Data = products;
                return Ok(response);
            }
            return BadRequest(response);
        }

      
    }
}
