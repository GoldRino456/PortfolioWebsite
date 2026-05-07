namespace portfolio.server.models;

public class ContactFormMessage
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }
}
