using student_testing_system.Data;
using student_testing_system.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace student_testing_system.Services.Users
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsAsync(string userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId);
        }
    }
}
