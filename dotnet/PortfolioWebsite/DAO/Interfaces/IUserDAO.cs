// IUserDAO.cs
using PortfolioWebsite.Models;
using System.Collections.Generic;

namespace PortfolioWebsite.DAO.Interfaces
{
    public interface IUserDAO
    {
        List<User> GetAllUsers();
        User GetUserById(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
