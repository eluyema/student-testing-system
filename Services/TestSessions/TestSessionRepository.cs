using Microsoft.EntityFrameworkCore;
using student_testing_system.Data;
using student_testing_system.Models;
using student_testing_system.Models.Questions;

namespace student_testing_system.Services.TestSessions
{
    public class TestSessionRepository : GenericRepository<TestSession>, ITestSessionRepository
    {
        public TestSessionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<AssignedQuestion>> GetAssignedQuestionsAsync(Guid testSessionId)
        {
            return await _context.AssignedQuestions
                                 .Include(aq => aq.Question)
                                 .ThenInclude(q => q.Answers)
                                 .Where(aq => aq.TestSessionId == testSessionId)
                                 .ToListAsync();
        }

        public async Task<int> CountByUserIdAsync(string userId)
        {
            return await _context.TestSessions.CountAsync(ts => ts.UserId == userId);
        }

        public async Task<IEnumerable<TestSession>> GetByUserAndTestIdAsync(string userId, Guid testId)
        {
            return await _context.TestSessions
                                 .Where(ts => ts.UserId == userId && ts.TestId == testId)
                                 .ToListAsync();
        }

        public async Task<int> GetTotalQuestionsCountAsync(Guid testSessionId)
        {
            return await _context.AssignedQuestions
                                 .Where(aq => aq.TestSessionId == testSessionId)
                                 .CountAsync();
        }
    }

}
