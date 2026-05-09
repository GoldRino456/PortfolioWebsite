using Microsoft.EntityFrameworkCore;

namespace portfolio.server.models;

public class PortfolioDbContext(DbContextOptions<PortfolioDbContext> options): DbContext(options)
{
    public DbSet<BlogPost> BlogPosts { get; set; }
}
