using Microsoft.EntityFrameworkCore;

namespace portfolio.server.models;

public class PortfolioDbContext(DbContextOptions options): DbContext(options)
{
    public DbSet<BlogPost> BlogPosts { get; set; }
}
