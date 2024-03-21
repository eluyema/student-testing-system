using student_testing_system.Models;
using student_testing_system.Services.UserAnswers.DTOs;

namespace student_testing_system.Services.UserAnswers
{
    public interface IUserAnswerService
    {
        Task<bool> AddUserAnswer(CreateUserAnswerDTO dto, string UserId);
        Task<IEnumerable<UserAnswerDTO>> GetAllUserAnswersForTestSession(Guid testSessionId);
    }
}
