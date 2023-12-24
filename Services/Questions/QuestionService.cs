using student_testing_system.Blob.Questions;
using student_testing_system.Models.Answers;
using student_testing_system.Models.Questions;
using student_testing_system.Services.Questions.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace student_testing_system.Services.Questions
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly string _fileDirectoryPath;

        public QuestionService(IQuestionRepository questionRepository, string fileDirectoryPath)
        {
            _questionRepository = questionRepository;
            _fileDirectoryPath = fileDirectoryPath;
        }

        public async Task<QuestionsListDTO> GetQuestionsByTestIdAsync(Guid testId)
        {
            var questions = await _questionRepository.GetQuestionsByTestIdAsync(testId);
            var questionsDto = questions.Select(q => new QuestionDTO
            {
                QuestionId = q.QuestionId,
                Text = q.Text,
                TestId = q.TestId,
                FileName = q.FileName,
                Answers = q.Answers.Select(a => new AnswerDTO
                {
                    AnswerId = a.AnswerId,
                    Text = a.Text,
                    IsCorrect = a.IsCorrect
                }).ToList()
            }).ToList();

            return new QuestionsListDTO { Questions = questionsDto };
        }

        public async Task<QuestionDTO> GetQuestionByIdAsync(Guid questionId)
        {
            var question = await _questionRepository.GetByIdAsync(questionId);
            if (question == null) return null;

            return new QuestionDTO
            {
                QuestionId = question.QuestionId,
                Text = question.Text,
                TestId = question.TestId,
                FileName = question.FileName,
            };
        }

        public async Task<QuestionDTO> GetQuestionWithAnswersByIdAsync(Guid questionId)
        {
            var question = await _questionRepository.GetQuestionWithAnswersByIdAsync(questionId);
            if (question == null) return null;

            return new QuestionDTO
            {
                QuestionId = question.QuestionId,
                Text = question.Text,
                TestId = question.TestId,
                FileName = question.FileName,
                Answers = question.Answers.Select(a => new AnswerDTO
                {
                    AnswerId = a.AnswerId,
                    Text = a.Text,
                    IsCorrect = a.IsCorrect
                }).ToList()
            };
        }

        public async Task<QuestionDTO> CreateQuestionWithAnswersAsync(Guid testId, CreateQuestionWithAnswersDTO dto)
        {
            if (dto.Answers.Count(a => a.IsCorrect) != 1)
            {
                throw new InvalidOperationException("There must be exactly one correct answer.");
            }

            var filePath = QuestionFileWriter.WriteToFile(dto, _fileDirectoryPath);

            var question = new Question
            {
                Text = dto.Text,
                TestId = testId,
                FileName = filePath,
                Answers = dto.Answers.Select(a => new Answer
                {
                    Text = a.Text,
                    IsCorrect = a.IsCorrect
                }).ToList()
            };

            var createdQuestion = await _questionRepository.CreateQuestionWithAnswersAsync(testId, question);
            return new QuestionDTO
            {
                QuestionId = createdQuestion.QuestionId,
                Text = createdQuestion.Text,
                TestId = createdQuestion.TestId,
                FileName = createdQuestion.FileName,
                Answers = createdQuestion.Answers.Select(a => new AnswerDTO
                {
                    AnswerId = a.AnswerId,
                    Text = a.Text,
                    IsCorrect = a.IsCorrect
                }).ToList()
            };
        }



        public async Task UpdateQuestionAsync(Guid questionId, UpdateQuestionDTO updateDto)
        {
            await _questionRepository.UpdateQuestionAsync(questionId, updateDto);
        }

        public async Task DeleteQuestionAsync(Guid questionId)
        {
            await _questionRepository.DeleteAsync(questionId);
        }

        public async Task AddAnswerToQuestionAsync(Guid questionId, CreateAnswerDTO createAnswerDto)
        {
            await _questionRepository.AddAnswerToQuestionAsync(questionId, createAnswerDto);
        }
    }
}
