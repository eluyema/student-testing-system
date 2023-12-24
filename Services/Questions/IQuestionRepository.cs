using student_testing_system.Data;
using student_testing_system.Models.Questions;
using student_testing_system.Services.Questions.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace student_testing_system.Services.Questions
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        Task<IEnumerable<Question>> GetQuestionsByTestIdAsync(Guid testId);
        Task<int> CountQuestionsByTestIdAsync(Guid testId);
        Task<Question> CreateQuestionWithAnswersAsync(Guid testId, Question question);
        Task<Question> GetQuestionWithAnswersByIdAsync(Guid questionId);
        Task UpdateQuestionAsync(Guid questionId, UpdateQuestionDTO updateDto);
        Task AddAnswerToQuestionAsync(Guid questionId, CreateAnswerDTO createAnswerDto);
        Task<List<Question>> GetRandomQuestionsByTestIdAsync(Guid testId, int numberOfQuestions);
        Task<bool> ExistsInTestSession(Guid assignedQuestionId, Guid testSessionId);
    }
}
