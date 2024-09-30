using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Discount;
using OnlineStore.Application.Interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;

        public DiscountController(IDiscountService discountService, IMapper mapper)
        {
            _discountService = discountService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        public IActionResult GetAll()
        {
            var discounts = _discountService.GetAllDiscounts();
            if (discounts.Any())
            {
                return Ok(new GeneralResponse<IEnumerable<DiscountDTO>>(IsSuccess: true, "Discounts retrieved successfully", discounts));
            }
            return BadRequest(new GeneralResponse<string>(IsSuccess: true, "There's no Discounts yet"));

        }

        [HttpGet("GetById/{id:int}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        public IActionResult GetById(int id)
        {

            var discount = _discountService.GetDiscountById(id);
            if (discount is not null)
            {
                return Ok(new GeneralResponse<DiscountDTO>(IsSuccess: true, "Discount retrieved successfully", discount));
            }

            return BadRequest(new GeneralResponse<DiscountDTO>(false, "Sorry we cant't find this Discount"));
        }

        [HttpPost("Add")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        public IActionResult Add([FromBody] AddDiscountDTO discountDTO)
        {
            var response = new GeneralResponse<AddDiscountDTO>(false, "Sorry, We can't Add This Discount", discountDTO);
            if (ModelState.IsValid)
            {

                int result = _discountService.AddDiscount(discountDTO);
                if (result > 0)
                {
                    response.Success = true;
                    response.Message = "Discount added successfully";
                    return Ok(response);
                }
            }
            return BadRequest(response);

        }

        [HttpPut("Update/{id:int}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        public IActionResult Update(int id, [FromBody] DiscountDTO discountDTO)
        {
            var result = 0;
            var response = new GeneralResponse<DiscountDTO>(false, "Sorry, We can't Update This Discount", discountDTO);
            if (ModelState.IsValid)
            {
                result = _discountService.UpdateDiscount(discountDTO);
                if (result > 0)
                {
                    response.Success = true;
                    response.Message = "Discount updated successfully";
                    return Ok(response);
                }
            }
            return BadRequest(response);
        }

        [HttpDelete("Delete/{id:int}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        public IActionResult Delete(int id)
        {
            var Result = 0;
            var response = new GeneralResponse<string>(false, "Sorry, We can't Delete This Discount");
            Result = _discountService.DeleteDiscount(id);
            if (Result > 0)
            {
                response.Success = true;
                response.Message = "Discount deleted successfully";
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("ApplyDiscount")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.BrandOwner)]
        public IActionResult ApplyDiscountToProduct(int productId, int discountId)
        {
            var response = new GeneralResponse<DiscountDTO>(false, "Sorry, We can't Apply This Discount");
            var result = 0;
            result = _discountService.ApplyDiscountToProduct(productId, discountId);
            if (result > 0)
            {
                response.Success=true;
                response.Message = "Discount applied to product successfully";
                return Ok(response);
            }
            return BadRequest(response);

        }
    }
}
