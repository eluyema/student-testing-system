using System.ComponentModel.DataAnnotations;

namespace student_testing_system.Services.Subjects.DTOs
{
    public class TestsListDTO
    {
        [Required]
        public ICollection<TestDTO> Tests { get; set; }
    }
}
