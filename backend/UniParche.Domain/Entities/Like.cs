namespace UniParche.Domain.Entities;

using UniParche.Domain.Enums;

public class Like : AuditBase
{
    // Foreign key
    public int UserId { get; set; }
    public int PostId { get; set; }

    // Reacción / Tipo de like
    public ReactionType ReactionType { get; set; } = ReactionType.Like;

    // navigation properties
    public User User { get; set; } = null!;
    public Post Post { get; set; } = null!;
}
