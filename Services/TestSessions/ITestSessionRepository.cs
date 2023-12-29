using student_testing_system.Data;
using student_testing_system.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace student_testing_system.Services.TestSessions
{
    public interface ITestSessionRepository : IGenericRepository<TestSession>
    {
        Task<int> CountByUserIdAsync(string userId);
        Task<List<AssignedQuestion>> GetAssignedQuestionsAsync(Guid testSessionId);

        Task<IEnumerable<TestSession>> GetByUserAndTestIdAsync(string userId, Guid testId);
        Task<int> GetTotalQuestionsCountAsync(Guid testSessionId);
    }
}
