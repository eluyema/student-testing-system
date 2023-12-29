using student_testing_system.Models.EF;
using student_testing_system.Models.Questions;
using System;

namespace student_testing_system.Models
{
    public class AssignedQuestion : BaseEntity
    {
        public Guid AssignedQuestionId { get; set; } = Guid.NewGuid();
        public Guid TestSessionId { get; set; }
        public Guid QuestionId { get; set; }

        public TestSession TestSession { get; set; }
        public Question Question { get; set; }
        public UserAnswer UserAnswer { get; set; }
}
}
