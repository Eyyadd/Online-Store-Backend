using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Cart;
using OnlineStore.Application.Interfaces;

namespace OnlineStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CartController : ControllerBase
    {
        private readonly ICartServices _cartServices;
        //private readonly string? UserId;

        public CartController(ICartServices cartServices)
        {
            _cartServices = cartServices;
            //UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        [HttpGet]
        public ActionResult<GeneralResponse<IEnumerable<RetriveCartItemsDTO>>> Cart()
        {
            GeneralResponse<IEnumerable<RetriveCartItemsDTO>> Response = new GeneralResponse<IEnumerable<RetriveCartItemsDTO>>(false, "An Error Accoured Please Try Again Later");
            var UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Data = _cartServices.Cart(UserID);
            if(Data is not null)
            {
                Response.Success = true;
                Response.Message = "Cart Retrived Successflly";
                Response.Data = Data;
            }
            return Response;
        }

        [HttpPost("AddToCart")]
        public GeneralResponse<IEnumerable<RetriveCartItemsDTO>> AddToCart( CreateCartItemDTO createCartItemDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Result =_cartServices.AddToCart(createCartItemDTO, userId);
            return new GeneralResponse<IEnumerable<RetriveCartItemsDTO>>(true, "", Result);
          
        }

        [HttpPut("UpdateCartItem")]
        public GeneralResponse<IEnumerable<RetriveCartItemsDTO>> updateCartItem (UpdateCartItemDTO updateCartItemDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Result = _cartServices.UpdateCartProudctQuantity(updateCartItemDTO, userId);
            return new GeneralResponse<IEnumerable<RetriveCartItemsDTO>>(true, "", Result);
        }

        [HttpDelete("DeleteCartItem/{cartItemId}")]
        public GeneralResponse<IEnumerable<RetriveCartItemsDTO>> deleteCartItem([FromQuery]int cartItemId)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Result = _cartServices.RemoveItemFromCart(cartItemId ,UserId);
            return new GeneralResponse<IEnumerable<RetriveCartItemsDTO>>(true, "", Result);
        }


        [HttpDelete("DeleteCart")]
        public async Task<GeneralResponse<int>> RemoveAllItems()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Result =await _cartServices.RemoveCartItems(UserId);
            return new GeneralResponse<int>(true, "" , Result);
        }
    }
}
