using Microsoft.AspNetCore.Mvc;
using ServiceAbstractions;
using Shared.DTO.IdentityModule;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
