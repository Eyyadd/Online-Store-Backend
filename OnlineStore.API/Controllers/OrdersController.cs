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
        public UserManager<User> _UserManager { get; set; }
        public OrdersController(IOrderService ownerService,UserManager<User> userManager)
        {
            this._OrderService = ownerService;
            this._UserManager = userManager;
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

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateOrderAsync()
        {
            var carId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user=await _UserManager.FindByIdAsync(carId);
            if (user.CartId is not null)
            {
                int crt = (int)user.CartId;
                var orderService = _OrderService.CreateOrder(crt);
                if (orderService is not null)
                {
                    return Ok(orderService);
                }
            }
            return BadRequest();
        }

    }
}