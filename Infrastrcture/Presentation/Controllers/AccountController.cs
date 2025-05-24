using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstractions;
using Shared.DTO.IdentityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            return Ok(await serviceManager.AuthenticationService.Register(registerDto));
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            return Ok(await serviceManager.AuthenticationService.Login(loginDto));
        }
        [HttpGet("IsEmailExisted")]
        public async Task<ActionResult<bool>> CheckUserEmailExist(string email)
        {
            return Ok(await serviceManager.AuthenticationService.CheckEmailExist(email));
        }
        [HttpGet("CuurentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCuurentUser()
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            return Ok(await serviceManager.AuthenticationService.GetCurrentUser(email));
        }

        [HttpGet("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetCuurentAddress()
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            return Ok(await serviceManager.AuthenticationService.GetCurrentUserAddress(email));
        }
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress([FromBody] AddressDto addressDto)
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            return Ok(await serviceManager.AuthenticationService.UpdateUserAddress(email, addressDto));
        }
    }
}
