using student_testing_system.Models.Answers;
using student_testing_system.Models.Questions;
using student_testing_system.Services.Questions.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace student_testing_system.Services.Questions
{
    public interface IQuestionService
    {
        Task<QuestionsListDTO> GetQuestionsByTestIdAsync(Guid testId);
        Task<QuestionDTO> GetQuestionByIdAsync(Guid questionId);
        Task<QuestionDTO> GetQuestionWithAnswersByIdAsync(Guid questionId);
        Task<QuestionDTO> CreateQuestionWithAnswersAsync(Guid testId, CreateQuestionWithAnswersDTO question);
        Task UpdateQuestionAsync(Guid questionId, UpdateQuestionDTO updateQuestionDto);
        Task DeleteQuestionAsync(Guid questionId);
        Task AddAnswerToQuestionAsync(Guid questionId, CreateAnswerDTO createAnswerDto);
    }
}
