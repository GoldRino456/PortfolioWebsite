namespace portfolio.server.models;

public class BlogPost
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdated {  get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public string? Slug { get; set; } //Slug should default to some value if null. Maybe creation date or title?
    public required string Content { get; set; }
    public bool IsPublished { get; set; }
}
