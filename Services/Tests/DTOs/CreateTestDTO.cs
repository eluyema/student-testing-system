using System.ComponentModel.DataAnnotations;

namespace student_testing_system.Services.Tests.DTOs
{
    public class CreateTestDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public Guid SubjectId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "AllowedAttempts must be at least 1.")]
        public int AllowedAttempts { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "MinutesDuration must be at least 1.")]
        public int MinutesDuration { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "QuestionsPerAttempt must be at least 1.")]
        public int QuestionsPerAttempt { get; set; }
    }
}
