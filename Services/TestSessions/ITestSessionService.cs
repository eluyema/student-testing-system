using student_testing_system.Models;
using student_testing_system.Services.TestSessions.DTOs;

namespace student_testing_system.Services.TestSessions
{
    public interface ITestSessionService
    {
        Task<TestSessionDTO> StartTestSession(Guid testId, string userId);
        Task<List<QuestionDTO>> GetAssignedQuestionsAsync(Guid testSessionId);
        Task<TestSessionsListDTO> GetTestSessionsByUserAndTestIdAsync(string userId, Guid testId);
        Task<TestSessionDTO> GetTestSessionByIdAsync(Guid testSessionId);
    }
}
