using Microsoft.AspNetCore.Mvc;
using student_testing_system.Services.Auth;
using student_testing_system.Services.Auth.DTOs;
using System.Threading.Tasks;

namespace student_testing_system.Controllers
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
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterUserAsync(registerDTO);

            if (result.Succeeded)
            {
                return Ok("User registered successfully.");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authService.LoginUserAsync(loginDTO);

            if (response.SignInResult.Succeeded)
            {
                return Ok(new { Token = response.Token });
            }


            return BadRequest("Invalid login attempt.");
        }

    }
}
