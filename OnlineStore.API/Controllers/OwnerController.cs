using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.User;
using OnlineStore.Application.Interfaces;

namespace OnlineStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet("GetAllOwners")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        public IActionResult GetAllOwners()
        {
            var response = new GeneralResponse<IEnumerable<UsersDTO>>(false, "No Owners yet", []);
            var owners = _ownerService.GetAllOwners();
            if (owners.Any())
            {
                response.Success = true;
                response.Message = "Owners Rterived Successfully";
                response.Data = owners;
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
