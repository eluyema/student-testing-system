using Microsoft.AspNetCore.Identity;
using student_testing_system.Services.Auth.DTOs;

namespace student_testing_system.Services.Auth
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterDTO registerDTO);
        Task<LoginResponseDTO> LoginUserAsync(LoginDTO loginDTO);
    }

}
