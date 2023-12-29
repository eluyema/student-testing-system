using student_testing_system.Models.EF;
using student_testing_system.Models.Questions;

namespace student_testing_system.Models.Answers
{
    public class Answer : BaseEntity
    {
        public Guid AnswerId { get; set; } = Guid.NewGuid();
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
