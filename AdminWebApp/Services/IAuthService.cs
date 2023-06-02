namespace AdminWebApp.Services
{
    namespace AdminWebApp.Services
    {
        public interface IAuthService
        {
            bool IsUserValid(string email, string password);
            void BlockUser(int userId);
            void UnblockUser(int userId);
            void DeleteUser(int userId);
            void CreateUser(string name, string email, string password);
        }
    }

}
