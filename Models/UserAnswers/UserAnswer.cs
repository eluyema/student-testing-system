using student_testing_system.Models.EF;
using student_testing_system.Models.Answers;
using System;

namespace student_testing_system.Models
{
    public class UserAnswer : BaseEntity
    {
        public Guid UserAnswerId { get; set; } = Guid.NewGuid();
        public Guid TestSessionId { get; set; }
        public Guid AssignedQuestionId { get; set; }

        public Guid AnswerId { get; set; }
        public bool IsCorrect { get; set; }
        public AssignedQuestion AssignedQuestion { get; set; }

        public TestSession TestSession { get; set; }
        public Answer Answer { get; set; }
    }
}
