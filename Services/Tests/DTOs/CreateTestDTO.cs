using System.ComponentModel.DataAnnotations;

namespace student_testing_system.Services.Tests.DTOs
{
    public class CreateTestDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int AllowedAttempts { get; set; }
        [Required]
        public int MinutesDuration { get; set; }
        [Required]
        public int QuestionsPerAttempt { get; set; }
    }

}
