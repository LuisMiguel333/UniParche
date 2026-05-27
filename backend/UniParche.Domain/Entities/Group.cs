using UniParche.Domain.Enums;

namespace UniParche.Domain.Entities;

public class Group : AuditBase
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int UniversityId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public int CreatorId { get; set; }
    public GroupType Type { get; set; } = GroupType.Study;

    // Navigation properties
    public University University { get; set; } = null!;
    public User Creator { get; set; } = null!;
    public ICollection<GroupMember> Members { get; set; } = new List<GroupMember>();
}
