using student_testing_system.Data;
using student_testing_system.Models;
using System;
using System.Threading.Tasks;

namespace student_testing_system.Services.UserAnswers
{
    public interface IUserAnswerRepository : IGenericRepository<UserAnswer>
    {
        Task<UserAnswer> FindByAssignedQuestionIdAsync(Guid assignedQuestionId);

        Task<IEnumerable<UserAnswer>> GetAllUserAnswersForTestSession(Guid testSessionId);

        Task<IEnumerable<UserAnswer>> GetByTestSessionIdAsync(Guid testSessionId);
    }
}
