using Microsoft.AspNetCore.Identity;
using student_testing_system.Models.Users;
using student_testing_system.Services.Auth.DTOs;
using student_testing_system.Services.Jwt;

namespace student_testing_system.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtService _jwtService;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, JwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterDTO model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Student");
            }

            return result;
        }


        public async Task<LoginResponseDTO> LoginUserAsync(LoginDTO model)
        {
            var response = new LoginResponseDTO
            {
                SignInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false)
            };

            if (response.SignInResult.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    response.Token = await _jwtService.GenerateTokenAsync(user);
                }
            }

            return response;
        }

    }

}
