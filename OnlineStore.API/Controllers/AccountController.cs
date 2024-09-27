using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MimeKit;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Services;
using OnlineStore.Domain.Utilities;
using OnlineStore.Infrastructure.DTOs;
using OnlineStore.Service.Helper;
using System.Web;

namespace OnlineStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _UserManager;
        private readonly IConfiguration _Configuration;
        private readonly IMapper _Mapper;
        private readonly ISendEmail sendEmailService;
        private readonly ICartServices _cartServices;
        private readonly IUserService _userService;

        public AccountController(UserManager<User> userManager
            ,IConfiguration configuration, IMapper mapper
            ,ISendEmail sendEmailService
            ,ICartServices cartServices
            ,IUserService userService)
        {
            _UserManager = userManager;
            _Configuration = configuration;
            _Mapper = mapper;
            this.sendEmailService = sendEmailService;
            _cartServices = cartServices;
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<GeneralResponse<string>>> Register(RegisterUserDTO user)
        {
            GeneralResponse<string> Response = new GeneralResponse<string>(false, "User Is Already Exsist!");
            

            var User = await _UserManager.FindByEmailAsync(user.Email);
            if (User is not null)
                return Response;

            User = await _UserManager.FindByNameAsync(user.Name);
            if (User is not null)
                return Response;
            User = new User()
            {
                UserName = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            var Result = await _UserManager.CreateAsync(User, user.Password);
            if(Result.Succeeded)
            {
                Cart cart =_cartServices.CreateCart(User);
                User.CartId = cart.Id;
                _UserManager.UpdateAsync(User);
                Response.Success = true;
                Response.Data = "Check Email For Confirmation";
                Response.Message = null;
                var token = await _UserManager.GenerateEmailConfirmationTokenAsync(User);
                var confirmationLink = $"{_Configuration["BaseURL"]}/api/Account/ConfirmEmail?Email={User.Email}&token={token}";
                Email email = new Email()
                {
                    To = User.Email,
                    Subject = "Confirm your email",
                    Body = $"Please confirm your account by clicking this link: <a href='{confirmationLink}'>Confirm Email</a>"
            };
                await this.sendEmailService.SendEmailAsync(email);
            }
           
            return Response;
        }

        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult<GeneralResponse<string>>> ConfirmEmail(string email , string token)
        {
            var Response =new  GeneralResponse<string>(false, "incorrect Email");
            var user = await _UserManager.FindByEmailAsync(email);
            if(user is not null)
            {
                var result = await _UserManager.ConfirmEmailAsync(user , token);
                if(result.Succeeded)
                {
                    Response.Success = true;
                    Response.Message = null;
                    Response.Data = "Email Confirmed Successfuly!";
                };
                return Response;
            }
            return Response;
        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDTO loginUserDTO)
        {
            if (ModelState.IsValid)
            {
                var ValidUser = await _UserManager.FindByNameAsync(loginUserDTO.UserName);
                if (ValidUser is not null)
                {
                    bool checkPassword = await _UserManager.CheckPasswordAsync(ValidUser, loginUserDTO.Password);
                    if (checkPassword)
                    {
                        //TODO Add the CartId And WishLIst ID 
                        List<Claim> Userclaims = new List<Claim>();
                        Userclaims.Add(new Claim(ClaimTypes.Name, ValidUser.UserName!));
                        Userclaims.Add(new Claim(ClaimTypes.NameIdentifier, ValidUser.Id));
                        Userclaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        var roles = await _UserManager.GetRolesAsync(ValidUser);
                        foreach (var role in roles)
                        {
                            Userclaims.Add(new Claim(ClaimTypes.Role, role));
                        }


                        SymmetricSecurityKey key =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["Jwt:key"]!));

                        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


                        JwtSecurityToken JwtToken = new JwtSecurityToken
                        (
                           issuer: _Configuration["Jwt:Issu"],
                           audience: _Configuration["Jwt:Aud"],
                           expires: DateTime.Now.AddDays(1),
                           claims: Userclaims,
                           signingCredentials: credentials

                        );
                        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                        var SuccessToken = new GeneratedToken(handler.WriteToken(JwtToken), JwtToken.ValidTo);
                        return Ok(new GeneralResponse<GeneratedToken>(true, "Login Successfully", SuccessToken));
                    }
                }
            }
            return Unauthorized(new GeneralResponse<string>(false,"User Not Found",loginUserDTO.UserName));
        }

        [HttpPost("UpdatePassword")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDTO updatePasswordDTO)
        {
            var user = await _UserManager.FindByNameAsync(updatePasswordDTO.UserName);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var passwordValid = await _UserManager.CheckPasswordAsync(user, updatePasswordDTO.CurrentPassword);
            if (!passwordValid)
            {
                return BadRequest("Current password is incorrect.");
            }

            var changePasswordResult = await _UserManager.ChangePasswordAsync(user, updatePasswordDTO.CurrentPassword, updatePasswordDTO.NewPassword);

            if (changePasswordResult.Succeeded)
            {
                return Ok("Password updated successfully.");
            }

            foreach (var error in changePasswordResult.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }


    }
}
