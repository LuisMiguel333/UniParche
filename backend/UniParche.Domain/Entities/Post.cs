namespace UniParche.Domain.Entities;

public class Post : AuditBase
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    // Foreign key
    public int UserId { get; set; }
    // navigation properties
    public User User { get; set; } = null!;
}
