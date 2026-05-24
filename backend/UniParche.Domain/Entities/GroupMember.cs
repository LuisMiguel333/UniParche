using UniParche.Domain.Enums;

namespace UniParche.Domain.Entities;

public class GroupMember : AuditBase
{
	public int GroupId { get; set; }
	public int UserId { get; set; }
	public GroupRole Role { get; set; } = GroupRole.Member;
	public DateTime JoinDate { get; set; } = DateTime.UtcNow;

	// Navigation properties
	public Group Group { get; set; } = null!;
	public User User { get; set; } = null!;
}