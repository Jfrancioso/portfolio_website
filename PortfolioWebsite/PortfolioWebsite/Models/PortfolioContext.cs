namespace PortfolioWebsite.Models;
using Microsoft.EntityFrameworkCore;

public class PortfolioContext : DbContext
{
    public PortfolioContext(DbContextOptions<PortfolioContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Comment> Comments { get; set; }
}
