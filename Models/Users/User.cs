using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace student_testing_system.Models.Users
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(100, ErrorMessage = "Full name length can't be more than 100 characters.")]
        public string FullName { get; set; }
    }
}
