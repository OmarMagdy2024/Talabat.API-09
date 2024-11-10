using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Talabat.API.Errors;
using Talabat.API.Helpers;
using Talabat.API.ModelDTO;
using Talabat.Core.Models;
using Talabat.Core.Models.Order;
using Talabat.Core.ServicesContract;

namespace Talabat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public readonly SignInManager<AppUser> _signinManager;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager, IMapper mapper,IAuthService authService)
        {
            _userManager = userManager;
            _signinManager = signInManager;
            _mapper = mapper;
            _authService = authService;
        }
        [HttpPost("SignOut")]
        public async Task<ActionResult<UserDTO>> SignOut(RegisterDTO register)
        {
            if ( EmailExist(register.Email).Result.Value)
            {
                return BadRequest(new APIResponse(404));
            }
            var user=new AppUser() 
            {
               DisplayName=register.DisplayName,
               Email=register.Email,
               PhoneNumber=register.PhoneNumber,
               UserName=register.DisplayName
            };
            var result=await _userManager.CreateAsync(user,register.password);
            if (result.Succeeded)
            {
                return Ok(new UserDTO()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = await _authService.CreateTokenAsync(user, _userManager)
                });
            }
            else { return Ok(BadRequest(new APIResponse(400))); }
        }
        [HttpPost("SignIn")]
        public async Task<ActionResult<UserDTO>> SignIn(SignInDTO signIn)
        {
            var user = await _userManager.FindByEmailAsync(signIn.Email);
            if (user != null)
            {
                var result = await _signinManager.CheckPasswordSignInAsync(user, signIn.Password,false);
                if (result.Succeeded)
                {
                    return Ok(new UserDTO()
                    {
                        DisplayName=user.DisplayName,
                        Email=user.Email,
                        Token=await _authService.CreateTokenAsync(user,_userManager)
                    });
                }
                else { return Unauthorized(new APIResponse(401)); }
            }
            else { return Unauthorized(new APIResponse(401)); }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email ?? string.Empty);
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound(new APIResponse(404));
            }
            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }

        [HttpGet("address")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Address>> GetUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email ?? string.Empty);
            var user = await _userManager.FindAddressByEmailAsync(User);
            return Ok(user.Address);
            
        }

        [HttpPut("address")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Address>> updateAddress(AddressDTO address)
        {
            var updateaddress = _mapper.Map<Address>(address);
            var user = await _userManager.FindAddressByEmailAsync(User);

            updateaddress.Id=user.Address.Id;
            user.Address = updateaddress;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(address);
            }
            return BadRequest(new APIResponse(400));
        }

        [HttpGet("emailexist")]
        public async Task<ActionResult<bool>> EmailExist(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;

        }

    }

}
