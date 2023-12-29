using student_testing_system.Data;
using student_testing_system.Models.Users;

namespace student_testing_system.Services.Users
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> ExistsAsync(string userId);
    }
}
