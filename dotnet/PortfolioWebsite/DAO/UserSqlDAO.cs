// UserSqlDAO.cs
using PortfolioWebsite.DAO.Interfaces;
using PortfolioWebsite.Models;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioWebsite.DAO
{
    public class UserSqlDAO : IUserDAO
    {
        private readonly PortfolioContext _context;

        public UserSqlDAO(PortfolioContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
