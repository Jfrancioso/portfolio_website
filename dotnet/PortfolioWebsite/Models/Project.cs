// Project.cs
using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string ProjectUrl { get; set; }

        public string SourceCodeUrl { get; set; }
    }
}
