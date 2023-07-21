using PortfolioWebsite.DAO.Interfaces;
using PortfolioWebsite.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PortfolioWebsite.DAO
{
    public class ProjectSqlDAO : IProjectDAO
    {
        private readonly string connectionString;

        public ProjectSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Project> GetAllProjects()
        {
            List<Project> projects = new List<Project>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM projects", conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Project project = MapToProject(reader);
                            projects.Add(project);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                throw ex;
            }

            return projects;
        }

        public Project GetProjectById(int id)
        {
            Project project = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM projects WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            project = MapToProject(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                throw ex;
            }

            return project;
        }

        public void CreateProject(Project project)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO projects (title, description, image_url, project_url, source_code_url) VALUES (@title, @description, @image_url, @project_url, @source_code_url)", conn);
                    cmd.Parameters.AddWithValue("@title", project.Title);
                    cmd.Parameters.AddWithValue("@description", project.Description);
                    cmd.Parameters.AddWithValue("@image_url", project.ImageUrl);
                    cmd.Parameters.AddWithValue("@project_url", project.ProjectUrl);
                    cmd.Parameters.AddWithValue("@source_code_url", project.SourceCodeUrl);
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

        public void UpdateProject(Project project)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE projects SET title = @title, description = @description, image_url = @image_url, project_url = @project_url, source_code_url = @source_code_url WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@title", project.Title);
                    cmd.Parameters.AddWithValue("@description", project.Description);
                    cmd.Parameters.AddWithValue("@image_url", project.ImageUrl);
                    cmd.Parameters.AddWithValue("@project_url", project.ProjectUrl);
                    cmd.Parameters.AddWithValue("@source_code_url", project.SourceCodeUrl);
                    cmd.Parameters.AddWithValue("@id", project.Id);
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

        public void DeleteProject(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM projects WHERE id = @id", conn);
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

        private Project MapToProject(SqlDataReader reader)
        {
            return new Project
            {
                Id = Convert.ToInt32(reader["id"]),
                Title = Convert.ToString(reader["title"]),
                Description = Convert.ToString(reader["description"]),
                ImageUrl = Convert.ToString(reader["image_url"]),
                ProjectUrl = Convert.ToString(reader["project_url"]),
                SourceCodeUrl = Convert.ToString(reader["source_code_url"])
                // Map more properties as needed
            };
        }
    }
}
