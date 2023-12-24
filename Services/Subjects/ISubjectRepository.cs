using student_testing_system.Data;
using student_testing_system.Models.Subjects;
using student_testing_system.Models.Tests;

namespace student_testing_system.Services.Subjects
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        Task AddTestToSubjectAsync(Guid subjectId, Test test);
        Task<IEnumerable<Subject>> GetAllSubjectsWithTestsAsync();
    }

}
