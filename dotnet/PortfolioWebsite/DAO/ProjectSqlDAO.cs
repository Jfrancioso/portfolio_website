// ProjectSqlDAO.cs
using PortfolioWebsite.DAO.Interfaces;
using PortfolioWebsite.Models;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioWebsite.DAO
{
    public class ProjectSqlDAO : IProjectDAO
    {
        private readonly PortfolioContext _context;

        public ProjectSqlDAO(PortfolioContext context)
        {
            _context = context;
        }

        public List<Project> GetAllProjects()
        {
            return _context.Projects.ToList();
        }

        public Project GetProjectById(int id)
        {
            return _context.Projects.FirstOrDefault(p => p.Id == id);
        }

        public void CreateProject(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        public void UpdateProject(Project project)
        {
            _context.Projects.Update(project);
            _context.SaveChanges();
        }

        public void DeleteProject(int id)
        {
            Project project = _context.Projects.FirstOrDefault(p => p.Id == id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                _context.SaveChanges();
            }
        }
    }
}
