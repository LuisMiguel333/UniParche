using UniParche.Domain.Enums;

namespace UniParche.Domain.Entities;

public class Friendship : AuditBase
{
	public int User1Id { get; set; }
	public int User2Id { get; set; }
	public FriendshipStatus Status { get; set; } = FriendshipStatus.Pending;
	public DateTime Date { get; set; } = DateTime.UtcNow;

	// Navigation properties
	public User User1 { get; set; } = null!;
	public User User2 { get; set; } = null!;
}