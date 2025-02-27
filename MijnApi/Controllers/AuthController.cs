using Microsoft.AspNetCore.Mvc;
using MijnApi.Models;
using MijnApi.Services;

namespace MijnApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            var result = _authService.Register(user);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var result = _authService.Login(user);
            if (!result.Success)
            {
                return Unauthorized(result.Message);
            }
            return Ok(result.Message);
        }
    }
}