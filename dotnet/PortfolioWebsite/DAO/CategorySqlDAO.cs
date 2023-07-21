using PortfolioWebsite.DAO.Interfaces;
using PortfolioWebsite.Interfaces;
using PortfolioWebsite.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PortfolioWebsite.DAO
{
    public class CategorySqlDAO : ICategoryDAO
    {
        private readonly string _connectionString;

        public CategorySqlDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IList<Category> GetCategories()
        {
            IList<Category> categories = new List<Category>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM categories;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Category newCategory = CreateCategoryFromReader(reader);
                        categories.Add(newCategory);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting categories: " + ex.Message);
            }

            return categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            Category category = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM categories WHERE id = @id;", conn);
                    cmd.Parameters.AddWithValue("@id", categoryId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        category = CreateCategoryFromReader(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting category by ID: " + ex.Message);
            }

            return category;
        }

        public Category AddCategory(Category newCategory)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO categories (name, description) VALUES (@name, @description);", conn);
                    cmd.Parameters.AddWithValue("@name", newCategory.Name);
                    cmd.Parameters.AddWithValue("@description", newCategory.Description);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding category: " + ex.Message);
                return null;
            }

            return newCategory;
        }

        public void UpdateCategory(Category category)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE categories SET name = @name, description = @description WHERE id = @id;", conn);
                    cmd.Parameters.AddWithValue("@id", category.Id);
                    cmd.Parameters.AddWithValue("@name", category.Name);
                    cmd.Parameters.AddWithValue("@description", category.Description);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating category: " + ex.Message);
            }
        }

        public bool DeleteCategory(int categoryId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM categories WHERE id = @id;", conn);
                    cmd.Parameters.AddWithValue("@id", categoryId);

                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting category: " + ex.Message);
                return false;
            }
        }

        private Category CreateCategoryFromReader(SqlDataReader reader)
        {
            Category newCategory = new Category();

            newCategory.Id = Convert.ToInt32(reader["id"]);
            newCategory.Name = Convert.ToString(reader["name"]);
            newCategory.Description = Convert.ToString(reader["description"]);

            return newCategory;
        }

        public Category GetCategory(int id)
        {
            throw new NotImplementedException();
        }
    }
}
