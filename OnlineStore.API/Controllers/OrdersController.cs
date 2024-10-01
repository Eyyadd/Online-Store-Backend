using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Order;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Services;

namespace OnlineStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public IOrderService _OrderService { get; set; }
        public OrdersController(IOrderService ownerService)
        {
            this._OrderService = ownerService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetOrders()
        {
            var response = new GeneralResponse<IEnumerable<GetOrderItems>>(false, "No orders placed yet");
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _OrderService.GetOrdersByUserID(user);
            if (orders is not null)
            {
                response.Success = true;
                response.Message = "Orders Returned Successfully";
                response.Data = orders;
                return Ok(orders);
            }
            return BadRequest(response);
        }
    }

}
