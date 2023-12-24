using student_testing_system.Data;
using student_testing_system.Models.Answers;

namespace student_testing_system.Services.Answers
{
    public interface IAnswerRepository : IGenericRepository<Answer>
    {
        Task<bool> IsValidAnswerForAssignedQuestion(Guid assignedQuestionId, Guid answerId);
        Task<bool> IsCorrectAnswer(Guid answerId);
    }
}
