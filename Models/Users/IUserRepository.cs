using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using student_testing_system.Models.Users;

namespace student_testing_system.Models.Users
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
    }
}
