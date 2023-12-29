using System.ComponentModel.DataAnnotations;

namespace student_testing_system.Services.Subjects.DTOs
{
    public class UpdateSubjectDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
