using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using student_testing_system.Models.Users;
using student_testing_system.Services.Auth.DTOs;
using student_testing_system.Services.Jwt;

namespace student_testing_system.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokensService _tokensService;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, TokensService tokensService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokensService = tokensService;
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

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to create new user.");
            }

            await _userManager.AddToRoleAsync(user, "Student");
            return result;
        }


        public async Task<TokenResponseDTO> LoginUserAsync(LoginDTO model)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (!signInResult.Succeeded)
            {
                throw new UnauthorizedAccessException("Username or password is incorrect.");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                string token = await _tokensService.GenerateTokenAsync(user);
                string refreshToken = _tokensService.GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = _tokensService.GetRefreshTokenExpiryTime();
                await _userManager.UpdateAsync(user);

                return new TokenResponseDTO
                {
                    Token = token,
                    RefreshToken = refreshToken
                };
            }

            throw new UnauthorizedAccessException("User not found.");
        }


        public async Task<TokenResponseDTO> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiryTime > DateTime.UtcNow);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }

            var newAccessToken = await _tokensService.GenerateTokenAsync(user);
            var newRefreshToken = _tokensService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new TokenResponseDTO
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

    }

}
