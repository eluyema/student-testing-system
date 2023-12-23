using System.ComponentModel.DataAnnotations;

namespace student_testing_system.Services.Auth.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "Full name length can't be more than 100 characters.")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "Email length can't be more than 100.")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }
    }

}
