using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using My_Cards.Contracts;
using My_Cards.Services;

namespace My_Cards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [Route("register")]
        public async Task<bool> Register([FromBody] RegisterDto request)
        {
            var registerResult = await _authService.RegisterAsync(request);
            return registerResult;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            var registerResult = await _authService.LoginAsync(request);
            if (registerResult == null)
            {
                return Unauthorized();
            }
            HttpContext.Response.Cookies.Append("qwe", registerResult.Token);
            return Ok(registerResult);
        }
    }
}
