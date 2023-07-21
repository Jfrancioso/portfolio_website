using PortfolioWebsite.Models;
using System.Collections.Generic;

namespace PortfolioWebsite.Interfaces
{
    public interface ICategoryDAO
    {
        IList<Category> GetCategories();
        Category GetCategory(int id);
        Category AddCategory(Category category);
        void UpdateCategory(Category category);
        bool DeleteCategory(int id);
    }
}
