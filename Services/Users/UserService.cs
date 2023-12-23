using student_testing_system.Models.Users;

namespace student_testing_system.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid id)
        {
            var dbUser = await _userRepository.GetUserByIdAsync(id);
            return new UserDTO { Id = dbUser.Id, Name = dbUser.Name };
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDTO)
        {
            var user = new User { Name = createUserDTO.Name };
            var createdUser = await _userRepository.CreateUserAsync(user);
            return new UserDTO { Id = createdUser.Id, Name = createdUser.Name };
        }

        public async Task UpdateUserAsync(Guid id, UpdateUserDTO updateUserDTO)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            user.Name = updateUserDTO.Name;
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteUserAsync(id);
        }
    }
}
