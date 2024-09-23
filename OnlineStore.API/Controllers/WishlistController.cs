using Microsoft.AspNetCore.Mvc;
using OnlineStore.Infrastructure.Services;
using OnlineStore.Infrastructure.DTOs;
using System;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(AuthenticationSchemes ="Bearer")]
        public IActionResult AddToWishlist(int ProductVariantId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            

            if (userId == null)
            {
                return BadRequest("Invalid User");
            }
                
            var result = _wishlistService.AddToWishlist(ProductVariantId, userId);
            return Ok(result);
        }

        [HttpDelete("Remove")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RemoveFromWishlist(int productVariantId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _wishlistService.RemoveFromWishlist(productVariantId, userId);
            return Ok("Item removed from wishlist.");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetWishlist()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var wishlist = _wishlistService.GetWishlistProducts(userId);
            return Ok(wishlist);
        }

        [HttpPost("Create")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _wishlistService.Create(userId);
            return Ok("Wishlist created or retrieved successfully.");
        }

    }
}
