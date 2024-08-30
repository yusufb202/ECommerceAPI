using Core.Models;
using Core.Repositories;
using Microsoft.Extensions.Configuration;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<User> AddUserAsync(User user)
        {
           return await _userRepository.AddAsync(user);
        }

        public async Task<User> ValidateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null || user.Password != password)
            {
                return null;
            }
            return user;
        }

        
    }
}
