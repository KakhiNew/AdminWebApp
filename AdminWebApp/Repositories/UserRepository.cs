﻿using AdminWebApp.Data;
using AdminWebApp.Entities;

namespace AdminWebApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUserById(int userId)
        {
            return _dbContext.Users.Find(userId);
        }

        public User GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        public List<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public void CreateUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }

        public void DeleteUser(int userId)
          {
            var user = _dbContext.Users.Find(userId);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
          }
        public void BlockUser(int userId)
        {
            var user = _dbContext.Users.Find(userId);
            if (user != null)
            {
                user.Blocked = true;
                _dbContext.SaveChanges();
            }
        }
        public void UnblockUser(int userId)
        {
            var user = _dbContext.Users.Find(userId);
            if (user != null)
            {
                user.Blocked = false; 
                _dbContext.SaveChanges();
            }
        }

        // public void DeleteUser(int userId)
        //  {
        //     var user = _dbContext.Users.Find(userId);
        //     if (user != null)
        //     {
        //         _dbContext.Users.Remove(user);
        //      _dbContext.SaveChanges();
        ///    }
        //  }

        //  public void DeleteUsers(List<int> userIds)
        // {
        //     var usersToDelete = _dbContext.Users.Where(u => userIds.Contains(u.Id));
        //     _dbContext.Users.RemoveRange(usersToDelete);
        //     _dbContext.SaveChanges();
        // }




    }
}
