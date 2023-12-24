using Microsoft.EntityFrameworkCore;
using student_testing_system.Data;
using student_testing_system.Models.Answers;
using student_testing_system.Models.Questions;
using student_testing_system.Services.Questions.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace student_testing_system.Services.Questions
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Question>> GetQuestionsByTestIdAsync(Guid testId)
        {
            return await _context.Questions
                .Where(q => q.TestId == testId)
                .Include(q => q.Answers)
                .ToListAsync();
        }

        public async Task<Question> CreateQuestionWithAnswersAsync(Guid testId, Question question)
        {
            var testExists = await _context.Tests.AnyAsync(t => t.TestId == testId);
            if (!testExists)
            {
                throw new KeyNotFoundException($"Test with ID {testId} not found.");
            }

            question.TestId = testId;
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return question;
        }

        public async Task<Question> GetQuestionWithAnswersByIdAsync(Guid questionId)
        {
            return await _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.QuestionId == questionId);
        }

        public async Task UpdateQuestionAsync(Guid questionId, UpdateQuestionDTO updateDto)
        {
            var question = await _context.Questions.FindAsync(questionId);
            if (question != null)
            {
                question.Text = updateDto.Text;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddAnswerToQuestionAsync(Guid questionId, CreateAnswerDTO createAnswerDto)
        {
            var question = await _context.Questions.FindAsync(questionId);
            if (question == null)
            {
                throw new KeyNotFoundException($"Question with ID {questionId} not found.");
            }

            var answer = new Answer
            {
                Text = createAnswerDto.Text,
                IsCorrect = createAnswerDto.IsCorrect,
                QuestionId = questionId
            };

            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
        }
    }
}
