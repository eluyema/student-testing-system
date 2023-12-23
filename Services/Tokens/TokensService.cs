using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using student_testing_system.Configurations;
using student_testing_system.Models.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace student_testing_system.Services.Jwt
{
    public class TokensService
    {
        private readonly AuthSettings _authSettings;
        private readonly UserManager<User> _userManager;

        public TokensService(IConfiguration config, UserManager<User> userManager)
        {
            _authSettings = new AuthSettings
            {
                SecretKey = config["Auth:SecretKey"],
                ExpiryInMinutes = int.Parse(config["Auth:ExpiryInMinutes"]),
                RefreshTokenExpiryInDays = int.Parse(config["Auth:RefreshTokenExpiryInDays"]),
            };
            _userManager = userManager;
        }

        public async Task<string> GenerateTokenAsync(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_authSettings.ExpiryInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public DateTime GetRefreshTokenExpiryTime()
        {
            return DateTime.UtcNow.AddDays(_authSettings.RefreshTokenExpiryInDays);
        }
    }
}
