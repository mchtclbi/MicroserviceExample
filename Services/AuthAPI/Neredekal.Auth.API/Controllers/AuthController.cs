using Microsoft.AspNetCore.Mvc;
using Neredekal.AuthAPI.Models.Request;
using Neredekal.AuthAPI.Service.Interfaces;

namespace Neredekal.Auth.API.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService= authService;
        }

        [HttpPost("api/auth/login")]
        public async Task<IActionResult> Login([FromBody] CreateTokenRequest request) => 
            Ok(await _authService.CreateToken(request));
    }
}