using PortfolioWebsite.Models;

namespace PortfolioWebsite.DAO.Interfaces
{
    public interface IUserDAO
    {
        User GetUser(string username);
        User CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        List<User> GetAllUsers();
        User GetUserById(int id);
        User AddUser(string username, string password, string role);
        User DeleteUser(string id);
    }
}
