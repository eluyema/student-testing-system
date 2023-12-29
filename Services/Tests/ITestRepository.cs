using student_testing_system.Data;
using student_testing_system.Models.Tests;

namespace student_testing_system.Services.Tests
{
    public interface ITestRepository : IGenericRepository<Test>
    {
        Task<IEnumerable<Test>> GetAllTestsBySubjectIdAsync(Guid subjectId);
    }
}
