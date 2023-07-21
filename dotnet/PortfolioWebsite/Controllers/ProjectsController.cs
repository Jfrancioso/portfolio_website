// ProjectsController.cs
using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.DAO.Interfaces;
using PortfolioWebsite.Interfaces;
using PortfolioWebsite.Models;
using System.Collections.Generic;

namespace PortfolioWebsite.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectDAO _projectDAO;

        public ProjectsController(IProjectDAO projectDAO)
        {
            _projectDAO = projectDAO;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Project>> GetAllProjects()
        {
            List<Project> projects = _projectDAO.GetAllProjects();

            if (projects.Count == 0)
            {
                return NoContent();
            }

            return Ok(projects);
        }

        [HttpGet("{id}")]
        public ActionResult<Project> GetProjectById(int id)
        {
            Project project = _projectDAO.GetProjectById(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public ActionResult<Project> CreateProject(Project newProject)
        {
            _projectDAO.CreateProject(newProject);

            return Ok(newProject);
        }

        [HttpPut("{id}")]
        public ActionResult<Project> UpdateProject(int id, Project updatedProject)
        {
            Project existingProject = _projectDAO.GetProjectById(id);

            if (existingProject == null)
            {
                return NotFound();
            }

            existingProject.Title = updatedProject.Title;
            existingProject.Description = updatedProject.Description;
            existingProject.ImageUrl = updatedProject.ImageUrl;
            existingProject.ProjectUrl = updatedProject.ProjectUrl;
            existingProject.SourceCodeUrl = updatedProject.SourceCodeUrl;

            _projectDAO.UpdateProject(existingProject);

            return Ok(existingProject);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProject(int id)
        {
            Project existingProject = _projectDAO.GetProjectById(id);

            if (existingProject == null)
            {
                return NotFound();
            }

            _projectDAO.DeleteProject(id);

            return NoContent();
        }
    }
}
