namespace UniParche.Domain.Entities;

public class Comment : AuditBase
{
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    // Foreign key
    public int UserId { get; set; }
    public int PostId { get; set; }
    // navigation properties
    public User User { get; set; } = null!;
    public Post Post { get; set; } = null!;
}
