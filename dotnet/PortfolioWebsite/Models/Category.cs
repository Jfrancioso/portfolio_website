using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
