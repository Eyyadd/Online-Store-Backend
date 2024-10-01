using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Interfaces;

namespace OnlineStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult CreateOrUpdatePaymentIntent(int CartId)
        {
            var Response = new GeneralResponse<string>(false, "Sorry wr can't complete payment");
            var cart = _paymentService.CreatePaymentIntent(CartId);
            if (cart is not null)
            {
                Response.Success = true;
                Response.Message = "Successfully";
                return Ok(Response);
            }
            return BadRequest(Response);
        }
    }
}
