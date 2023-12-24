using student_testing_system.Models.EF;
using student_testing_system.Models.Subjects;

namespace student_testing_system.Models.Tests
{
    public class Test : BaseEntity
    {
        public Guid TestId { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int AllowedAttempts { get; set; }
        public int MinutesDuration { get; set; }
        public int QuestionsPerAttempt { get; set; }
    }
}
