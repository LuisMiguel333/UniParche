using UniParche.Domain.Enums;

namespace UniParche.Domain.Entities;

public class GroupMember : AuditBase
{
    public int Id { get; set; }
    public int GroupId { get; set; }
    public int UserId { get; set; }
    public string Role { get; set; } = "Member";
    public DateTime JoinDate { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Group Group { get; set; } = null!;
    public User User { get; set; } = null!;
}
