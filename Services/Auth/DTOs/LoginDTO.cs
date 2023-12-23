using System.ComponentModel.DataAnnotations;

namespace student_testing_system.Services.Auth.DTOs
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

}
