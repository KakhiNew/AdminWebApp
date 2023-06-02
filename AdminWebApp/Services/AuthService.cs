
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
            if (user != null && VerifyPasswordHash(password, user.PasswordHash))
            {
                user.LastLoginTime = DateTime.Now;
                _userRepository.UpdateUser(user);
                return true;
            }
            return false;
        }

        public void BlockUser(int userId)
        {
            throw new NotImplementedException();
        }

        public void UnblockUser(int userId)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int userId)
        {
            throw new NotImplementedException();
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
