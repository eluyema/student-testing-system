using Microsoft.AspNetCore.Identity;

namespace student_testing_system.Services.Auth.DTOs
{
    public class LoginResponseDTO
    {
        public SignInResult SignInResult { get; set; }
        public string Token { get; set; }
    }

}
