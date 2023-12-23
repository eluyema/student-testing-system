using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using student_testing_system.Models.Users;

namespace student_testing_system.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(string id);
        Task<UserDTO> CreateUserAsync(CreateUserDTO user);
        Task UpdateUserAsync(string id, UpdateUserDTO updateUserDTO);
        Task DeleteUserAsync(string id);
    }
}
