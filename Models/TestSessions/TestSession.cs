using student_testing_system.Models.Answers;
using student_testing_system.Models.EF;
using student_testing_system.Models.Tests;
using student_testing_system.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace student_testing_system.Models
{
    public class TestSession : BaseEntity
    {
        public Guid TestSessionId { get; set; } = Guid.NewGuid();
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public Guid TestId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ICollection<AssignedQuestion> AssignedQuestions { get; set; } = new List<AssignedQuestion>();

        public virtual User User { get; set; }
        public virtual Test Test { get; set; }
    }
}
