using student_testing_system.Models.EF;
using student_testing_system.Models.Tests;
using student_testing_system.Models.Answers;

namespace student_testing_system.Models.Questions
{
    public class Question : BaseEntity
    {
        public Guid QuestionId { get; set; } = Guid.NewGuid();
        public string Text { get; set; }
        public Guid TestId { get; set; }
        public Test Test { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}
