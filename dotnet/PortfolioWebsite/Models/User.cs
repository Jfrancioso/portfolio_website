using PortfolioWebsite.Security.Models;

namespace PortfolioWebsite.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; } // Add this property
        public string Salt { get; set; } // Add this property
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
