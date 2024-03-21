using student_testing_system.Data;
using student_testing_system.Models.Answers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace student_testing_system.Services.Answers
{
    public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> IsValidAnswerForAssignedQuestion(Guid assignedQuestionId, Guid answerId)
        {
            var isValidAnswer = await _context.AssignedQuestions
                .Include(aq => aq.Question)
                .ThenInclude(q => q.Answers)
                .AnyAsync(aq => aq.AssignedQuestionId == assignedQuestionId &&
                                aq.Question.Answers.Any(a => a.AnswerId == answerId));

            return isValidAnswer;
        }

        public async Task<bool> IsCorrectAnswer(Guid answerId)
        {
            var answer = await _context.Answers.FindAsync(answerId);
            return answer != null && answer.IsCorrect;
        }


    }
}
