using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Interfaces;

namespace OnlineStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly UserManager<User> _userManager;
        public PaymentController(IPaymentService paymentService,UserManager<User> userManager)
        {
            _paymentService = paymentService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes ="Bearer")]
        public async Task<IActionResult> CreateOrUpdatePaymentIntent()
        {
            var Response = new GeneralResponse<string>(false, "Sorry wr can't complete payment");
            var carId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userManager.FindByIdAsync(carId);
            int cartId = (int)user.CartId;
            var cart = _paymentService.CreatePaymentIntent(cartId);
            if (cart is not null)
            {
                Response.Success = true;
                Response.Message = "Successfully";
                Response.Data = cart.ClientSecret;
                return Ok(Response);
            }
            return BadRequest(Response);
        }
    }
}
