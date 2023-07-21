// IProjectDAO.cs
using PortfolioWebsite.Models;
using System.Collections.Generic;

namespace PortfolioWebsite.DAO.Interfaces
{
    public interface IProjectDAO
    {
        List<Project> GetAllProjects();
        Project GetProjectById(int id);
        void CreateProject(Project project);
        void UpdateProject(Project project);
        void DeleteProject(int id);
    }
}
