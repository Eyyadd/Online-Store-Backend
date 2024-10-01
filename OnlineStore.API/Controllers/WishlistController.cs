using Microsoft.AspNetCore.Mvc;
using OnlineStore.Infrastructure.Services;
using OnlineStore.Infrastructure.DTOs;
using System;
using Microsoft.AspNetCore.Authorization;
using OnlineStore.Application.DTOs.Wishlist;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.DTOs.Products;

namespace OnlineStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpPost("AddToWishlist")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult AddToWishlist(int ProductVariantId)
        {
            var Response = new GeneralResponse<bool>(false, "Sorry we can't Add this items");
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = _wishlistService.AddToWishlist(ProductVariantId, userId);
            if(result)
            {
                Response.Success = true;
                Response.Message = "Items Added Successfully";
                Response.Data= result;
                return Ok(Response);
            }
            return BadRequest(Response);
        }

        [HttpDelete("Remove")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult RemoveFromWishlist(int productVariantId)
        {
            var Reesponse = new GeneralResponse<string>(false, "Sorry we can't delete this items","no Data");
            int result = 0;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           result= _wishlistService.RemoveFromWishlist(productVariantId, userId);
            if (result > 0)
            {
                Reesponse.Success = true;
                Reesponse.Message = "Item removed from wishlist Successfully";
                return Ok(Reesponse);
            }
            return BadRequest(Reesponse);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetWishlist()
        {
            var Response = new GeneralResponse<IEnumerable<ProductElementDTO>>(false, "Sorry we can't return this items", []);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wishlist = _wishlistService.GetWishlistProducts(userId);
            if (wishlist.Any())
            {
                Response.Success = true;
                Response.Message = "Items Retrived Successfully";
                Response.Data = wishlist;
                return Ok(Response);
            }
            return BadRequest(Response);
            
        }

        [HttpPost("Create")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Create()
        {
            var Response = new GeneralResponse<CreatedWishlistDTO>(false, "The Wishlist does not created");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           var Result = _wishlistService.Create(userId);
            if (Result is not null)
            {
                Response.Success = true;
                Response.Message = "Wishlist created or retrieved successfully.";
                Response.Data = Result;
                return Ok(Response);
            }
            return BadRequest(Response);
        }

    }
}
