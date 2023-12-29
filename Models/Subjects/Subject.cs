using student_testing_system.Models.EF;
using student_testing_system.Models.Tests;

namespace student_testing_system.Models.Subjects
{
    public class Subject : BaseEntity
    {
        public Guid SubjectId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public ICollection<Test> Tests { get; set; }
    }
}
