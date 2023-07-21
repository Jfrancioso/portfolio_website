// UsersController.cs
using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.DAO.Interfaces;
using PortfolioWebsite.Interfaces;
using PortfolioWebsite.Models;
using System.Collections.Generic;

namespace PortfolioWebsite.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserDAO _userDAO;

        public UsersController(IUserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            List<User> users = _userDAO.GetAllUsers();

            if (users.Count == 0)
            {
                return NoContent();
            }

            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            User user = _userDAO.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User newUser)
        {
            _userDAO.CreateUser(newUser);

            return Ok(newUser);
        }

        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(int id, User updatedUser)
        {
            User existingUser = _userDAO.GetUserById(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Username = updatedUser.Username;
            existingUser.PasswordHash = updatedUser.PasswordHash;
            existingUser.Email = updatedUser.Email;
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;

            _userDAO.UpdateUser(existingUser);

            return Ok(existingUser);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            User existingUser = _userDAO.GetUserById(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            _userDAO.DeleteUser(id);

            return NoContent();
        }
    }
}
