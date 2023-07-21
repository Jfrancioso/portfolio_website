// UserSqlDAO.cs
using PortfolioWebsite.DAO.Interfaces;
using PortfolioWebsite.Models;
using PortfolioWebsite.Security;
using PortfolioWebsite.Security.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PortfolioWebsite.DAO
{
    public class UserSqlDAO : IUserDAO
    {
        private readonly string connectionString;

        public UserSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public User GetUser(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE username = @username", conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Read user information from the database
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            string storedUsername = reader.GetString(reader.GetOrdinal("username"));
                            string passwordHash = reader.GetString(reader.GetOrdinal("password_hash"));
                            string salt = reader.GetString(reader.GetOrdinal("salt"));
                            string email = reader.GetString(reader.GetOrdinal("email"));
                            string firstName = reader.GetString(reader.GetOrdinal("first_name"));
                            string lastName = reader.GetString(reader.GetOrdinal("last_name"));

                            // Create and return a User object with the retrieved information
                            User user = new User
                            {
                                Id = id,
                                Username = storedUsername,
                                PasswordHash = passwordHash,
                                Salt = salt,
                                Email = email,
                                FirstName = firstName,
                                LastName = lastName
                            };

                            return user;
                        }
                    }
                }
            }
            catch (SqlException)
            {
                // Handle any SQL exceptions that might occur during database operations
                throw;
            }

            // Return null if no user with the specified username is found
            return null;
        }


        public User CreateUser(User user)
        {
            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(user.Password);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO users (username, password_hash, salt, email, first_name, last_name) VALUES (@username, @password_hash, @salt, @email, @first_name, @last_name)", conn);
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                    cmd.Parameters.AddWithValue("@salt", hash.Salt);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@first_name", user.FirstName);
                    cmd.Parameters.AddWithValue("@last_name", user.LastName);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return GetUser(user.Username);
        }

        public void UpdateUser(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE users SET username = @username, password_hash = @password_hash, salt = @salt, email = @email, first_name = @first_name, last_name = @last_name WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password_hash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@salt", user.Salt);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@first_name", user.FirstName);
                    cmd.Parameters.AddWithValue("@last_name", user.LastName);
                    cmd.Parameters.AddWithValue("@id", user.Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM users WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public User AddUser(string username, string password, string role)
        {
            throw new NotImplementedException();
        }

        public User DeleteUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}
