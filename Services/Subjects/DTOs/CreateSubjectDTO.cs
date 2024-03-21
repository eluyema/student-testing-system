using System.ComponentModel.DataAnnotations;

namespace student_testing_system.Services.Subjects.DTOs
{
    public class CreateSubjectDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
