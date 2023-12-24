using student_testing_system.Models.Subjects;

namespace student_testing_system.Services.Subjects.DTOs
{
    public class TestDTO
    {
        public Guid TestId { get; set; }
        public string Title { get; set; }
        public int SubjectId { get; set; }

        public int AllowedAttempts { get; set; }
        public int MinutesDuration { get; set; }
        public int QuestionsPerAttempt { get; set; }
    }
}
