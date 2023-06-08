
using AdminWebApp.Entities;
using AdminWebApp.Repositories;
using AdminWebApp.Services.AdminWebApp.Services;

namespace AdminWebApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void CreateUser(string name, string email, string password)
        {
            var user = new User()
            {
                Email = email,
                RegistrationTime = DateTime.Now,
                Name = name,
                PasswordHash = HashPassword(password)
            };

            _userRepository.CreateUser(user);
        }

        public bool IsUserValid(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user != null && !user.Blocked && VerifyPasswordHash(password, user.PasswordHash))
            {
                user.LastLoginTime = DateTime.Now;
                _userRepository.UpdateUser(user);
                return true;
            }
            return false;
        }

        public void BlockUser(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null)
            {
                user.Blocked = true;
                _userRepository.UpdateUser(user);
            }
        }

        public void UnblockUser(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null)
            {
                user.Blocked = false;
                _userRepository.UpdateUser(user);
            }
        }

        public void DeleteUser(int userId)
        {
            _userRepository.DeleteUser(userId);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashString(password);
        }

        private bool VerifyPasswordHash(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
