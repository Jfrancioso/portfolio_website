using PortfolioWebsite.DAO.Interfaces;
using PortfolioWebsite.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PortfolioWebsite.DAO
{
    public class CommentSqlDAO : ICommentDAO
    {
        private readonly string connectionString;

        public CommentSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public void AddComment(Comment comment)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO comments (user_id, post_id, content, created_at) VALUES (@user_id, @post_id, @content, @created_at)", conn);
                    cmd.Parameters.AddWithValue("@user_id", comment.UserId);
                    cmd.Parameters.AddWithValue("@post_id", comment.PostId);
                    cmd.Parameters.AddWithValue("@content", comment.Content);
                    cmd.Parameters.AddWithValue("@created_at", comment.CreatedAt);
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

        public void DeleteComment(int commentId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM comments WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", commentId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                throw ex;
            }
        }

        public List<Comment> GetCommentsByPostId(int postId)
        {
            List<Comment> comments = new List<Comment>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM comments WHERE post_id = @post_id", conn);
                    cmd.Parameters.AddWithValue("@post_id", postId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Comment comment = MapToComment(reader);
                            comments.Add(comment);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                throw ex;
            }

            return comments;
        }

        public Comment GetCommentById(int commentId)
        {
            Comment comment = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM comments WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", commentId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            comment = MapToComment(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                throw ex;
            }

            return comment;
        }

        public void UpdateComment(Comment comment)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE comments SET user_id = @user_id, post_id = @post_id, content = @content, created_at = @created_at WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@user_id", comment.UserId);
                    cmd.Parameters.AddWithValue("@post_id", comment.PostId);
                    cmd.Parameters.AddWithValue("@content", comment.Content);
                    cmd.Parameters.AddWithValue("@created_at", comment.CreatedAt);
                    cmd.Parameters.AddWithValue("@id", comment.Id);
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

        public List<Comment> GetAllComments()
        {
            List<Comment> comments = new List<Comment>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM comments", conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Comment comment = MapToComment(reader);
                            comments.Add(comment);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                throw ex;
            }

            return comments;
        }

        private Comment MapToComment(SqlDataReader reader)
        {
            return new Comment
            {
                Id = Convert.ToInt32(reader["id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                PostId = Convert.ToInt32(reader["post_id"]),
                Content = Convert.ToString(reader["content"]),
                CreatedAt = Convert.ToDateTime(reader["created_at"])
                // Map more properties as needed
            };
        }
    }
}
