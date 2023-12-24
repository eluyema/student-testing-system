using Microsoft.EntityFrameworkCore;
using student_testing_system.Data;
using student_testing_system.Models.Subjects;
using student_testing_system.Models.Tests;

namespace student_testing_system.Services.Subjects
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddTestToSubjectAsync(Guid subjectId, Test test)
        {
            var subject = await _context.Subjects.Include(s => s.Tests).FirstOrDefaultAsync(s => s.SubjectId == subjectId);
            if (subject == null)
            {
                throw new KeyNotFoundException($"Subject with ID {subjectId} not found");
            }

            if (subject.Tests == null)
            {
                subject.Tests = new List<Test>();
            }

            subject.Tests.Add(test);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsWithTestsAsync()
        {
            return await _context.Subjects
                                 .Include(s => s.Tests)
                                 .ToListAsync();
        }
    }

}
