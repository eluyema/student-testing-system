using System.ComponentModel.DataAnnotations;

namespace student_testing_system.Services.Tests.DTOs
{
    public class QuestionsListDTO
    {
        [Required]
        public List<QuestionDTO> Questions { get; set; }
    }
}
