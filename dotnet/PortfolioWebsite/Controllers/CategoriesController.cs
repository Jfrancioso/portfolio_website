using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.Interfaces;
using PortfolioWebsite.Models;
using System.Collections.Generic;

namespace PortfolioWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryDAO _categoryDAO;

        public CategoriesController(ICategoryDAO categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAllCategories()
        {
            IEnumerable<Category> categories = _categoryDAO.GetCategories();

            if (categories == null)
            {
                return NoContent();
            }

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetCategoryById(int id)
        {
            Category category = _categoryDAO.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public ActionResult<Category> AddNewCategory(Category newCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Category category = _categoryDAO.AddCategory(newCategory);

            if (category == null)
            {
                return NoContent();
            }

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public ActionResult<Category> UpdateCategory(int id, Category updatedCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Category category = _categoryDAO.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = updatedCategory.Name;
            category.Description = updatedCategory.Description;

            _categoryDAO.UpdateCategory(category);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            Category category = _categoryDAO.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            bool result = _categoryDAO.DeleteCategory(id);

            if (result)
            {
                return NoContent();
            }

            return StatusCode(500); // Or any other appropriate error status code
        }
    }
}
