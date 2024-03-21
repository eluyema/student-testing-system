using Microsoft.AspNetCore.Identity;
using student_testing_system.Data;
using student_testing_system.Models.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace student_testing_system.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager; 

        public UserService(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return new UserDTO { Id = user.Id, UserName = user.UserName };
            }
            else
            {
                throw new KeyNotFoundException("User not found.");
            }
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDTO)
        {
            var user = new User { UserName = createUserDTO.UserName };
            var result = await _userManager.CreateAsync(user, createUserDTO.Password); 

            if (result.Succeeded)
            {
                return new UserDTO { Id = user.Id, UserName = user.UserName };
            }
            else
            {
                throw new Exception("User creation failed.");
            }
        }

        public async Task UpdateUserAsync(string id, UpdateUserDTO updateUserDTO)
        {
            var userIdAsString = id.ToString();
            var user = await _userManager.FindByIdAsync(userIdAsString);
            if (user != null)
            {
                user.UserName = updateUserDTO.UserName;
                await _userManager.UpdateAsync(user);
            }
            else
            {
                throw new KeyNotFoundException("User not found.");
            }
        }

        public async Task DeleteUserAsync(string id)
        {
            var userIdAsString = id.ToString();
            var user = await _userManager.FindByIdAsync(userIdAsString);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            else
            {
                throw new KeyNotFoundException("User not found.");
            }
        }
    }
}
