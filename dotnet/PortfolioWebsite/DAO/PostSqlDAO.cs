using PortfolioWebsite.DAO.Interfaces;
using PortfolioWebsite.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PortfolioWebsite.DAO
{
    public class PostSqlDAO : IPostDAO
    {
        private readonly string connectionString;

        public PostSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Post> GetAllPosts()
        {
            List<Post> posts = new List<Post>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM posts", conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Post post = MapToPost(reader);
                            posts.Add(post);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                throw ex;
            }

            return posts;
        }

        public Post GetPostById(int id)
        {
            Post post = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM posts WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            post = MapToPost(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                throw ex;
            }

            return post;
        }

        public void CreatePost(Post post)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO posts (user_id, category_id, title, content, created_at, updated_at) VALUES (@user_id, @category_id, @title, @content, @created_at, @updated_at)", conn);
                    cmd.Parameters.AddWithValue("@user_id", post.UserId);
                    cmd.Parameters.AddWithValue("@category_id", post.CategoryId);
                    cmd.Parameters.AddWithValue("@title", post.Title);
                    cmd.Parameters.AddWithValue("@content", post.Content);
                    cmd.Parameters.AddWithValue("@created_at", post.CreatedAt);
                    cmd.Parameters.AddWithValue("@updated_at", post.UpdatedAt);
                    // Add more parameters as needed
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                throw ex;
            }
        }

        public void UpdatePost(Post post)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE posts SET user_id = @user_id, category_id = @category_id, title = @title, content = @content, created_at = @created_at, updated_at = @updated_at WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@user_id", post.UserId);
                    cmd.Parameters.AddWithValue("@category_id", post.CategoryId);
                    cmd.Parameters.AddWithValue("@title", post.Title);
                    cmd.Parameters.AddWithValue("@content", post.Content);
                    cmd.Parameters.AddWithValue("@created_at", post.CreatedAt);
                    cmd.Parameters.AddWithValue("@updated_at", post.UpdatedAt);
                    cmd.Parameters.AddWithValue("@id", post.Id);
                    // Add more parameters as needed
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                throw ex;
            }
        }

        public void DeletePost(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM posts WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                throw ex;
            }
        }

        private Post MapToPost(SqlDataReader reader)
        {
            return new Post
            {
                Id = Convert.ToInt32(reader["id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                CategoryId = Convert.ToInt32(reader["category_id"]),
                Title = Convert.ToString(reader["title"]),
                Content = Convert.ToString(reader["content"]),
                CreatedAt = Convert.ToDateTime(reader["created_at"]),
                UpdatedAt = Convert.ToDateTime(reader["updated_at"])
                // Map more properties as needed
            };
        }
    }
}
