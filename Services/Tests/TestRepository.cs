using Microsoft.EntityFrameworkCore;
using student_testing_system.Data;
using student_testing_system.Models.Tests;

namespace student_testing_system.Services.Tests
{
    public class TestRepository : GenericRepository<Test>, ITestRepository
    {
        public TestRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Test>> GetAllTestsBySubjectIdAsync(Guid subjectId)
        {
            return await _context.Tests
                                 .Where(s => s.SubjectId == subjectId)
                                 .ToListAsync();
        }
    }
}
