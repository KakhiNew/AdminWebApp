using AdminWebApp.Entities;

namespace AdminWebApp.Repositories
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
        User GetUserById(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        List<User> GetAllUsers();
        void BlockUser(int userId);
        void UnblockUser(int userId);
    }
}

