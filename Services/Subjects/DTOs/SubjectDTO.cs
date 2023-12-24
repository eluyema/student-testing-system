using System.ComponentModel.DataAnnotations;

namespace student_testing_system.Services.Subjects.DTOs
{
    public class SubjectDTO
    {
        [Required]
        public Guid SubjectId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
