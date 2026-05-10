using Microsoft.EntityFrameworkCore;
using portfolio.server.models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PortfolioDbContext>(options => options.UseMySql(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/blogposts", async(int page = 1, int pageSize = 10, PortfolioDbContext dbContext) =>
{
    page = Math.Max(1, page);
    pageSize = Math.Min(50, pageSize);

    var query = dbContext.BlogPosts
        .Where(post => post.IsPublished)
        .OrderByDescending(post => post.CreatedAt);

    var totalRecords = await query.CountAsync();
    var posts = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    var (quotient, remainder) = int.DivRem(totalRecords, pageSize);
    var totalPages = remainder > 0 ? quotient + 1 : quotient;

    PagedResponse<BlogPost> response = new(posts, page, pageSize, totalRecords, totalPages);

    return response;
});

app.MapGet("api/blogposts/{id}", async(Guid id, PortfolioDbContext dbContext) => 
{
    return await dbContext.BlogPosts.FirstOrDefaultAsync(post => post.Id == id);
});

app.Run();