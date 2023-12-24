using student_testing_system.Models.Subjects;

namespace student_testing_system.Services.Subjects.DTOs
{
    public class CreateTestDTO
    {
        public string Title { get; set; }

        public int AllowedAttempts { get; set; }
        public int MinutesDuration { get; set; }
        public int QuestionsPerAttempt { get; set; }
    }
}
