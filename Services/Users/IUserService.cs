using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using student_testing_system.Models.Users;

namespace student_testing_system.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(Guid id);
        Task<UserDTO> CreateUserAsync(CreateUserDTO user);
        Task UpdateUserAsync(Guid id, UpdateUserDTO updateUserDTO);
        Task DeleteUserAsync(Guid id);
    }
}
