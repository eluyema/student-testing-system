using System.ComponentModel.DataAnnotations;

namespace student_testing_system.Services.Auth.DTOs
{
    public class RefreshTokenDTO
    {
        [Required]
        public string RefreshToken { get; set; }
    }

}
