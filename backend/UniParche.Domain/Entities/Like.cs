namespace UniParche.Domain.Entities;

internal class Like : AuditBase
{
    // Foreign key
    public int UserId { get; set; }
    public int PostId { get; set; }

    // navigation properties
    public User User { get; set; } = null!;
    public Post Post { get; set; } = null!;
}
