using student_testing_system.Data;
using student_testing_system.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace student_testing_system.Services.UserAnswers
{
    public class UserAnswerRepository : GenericRepository<UserAnswer>, IUserAnswerRepository
    {
        public UserAnswerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<UserAnswer> FindByAssignedQuestionIdAsync(Guid assignedQuestionId)
        {
            return await _context.UserAnswers
                                 .FirstOrDefaultAsync(ua => ua.AssignedQuestionId == assignedQuestionId);
        }

        public async Task<IEnumerable<UserAnswer>> GetAllUserAnswersForTestSession(Guid testSessionId)
        {
            return await _context.UserAnswers
                                 .Where(ua => ua.TestSessionId == testSessionId)
                                 .Include(ua => ua.Answer)
                                 .Include(ua => ua.AssignedQuestion)
                                     .ThenInclude(aq => aq.Question)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<UserAnswer>> GetByTestSessionIdAsync(Guid testSessionId)
        {
            return await _context.UserAnswers
                                 .Where(ua => ua.TestSessionId == testSessionId)
                                 .ToListAsync();
        }

    }
}
