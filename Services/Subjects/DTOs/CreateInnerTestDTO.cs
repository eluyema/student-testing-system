using student_testing_system.Models.Subjects;
using System.ComponentModel.DataAnnotations;

namespace student_testing_system.Services.Subjects.DTOs
{
    public class CreateInnerTestDTO
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
