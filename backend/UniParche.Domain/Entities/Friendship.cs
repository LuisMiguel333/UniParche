using UniParche.Domain.Enums;

namespace UniParche.Domain.Entities;

<<<<<<< HEAD
public class Friendship
{
    public int Id { get; set; }
    public int IdUser1 { get; set; }
    public int IdUser2 { get; set; }
    public FriendshipStatus Status { get; set; } = FriendshipStatus.Pending;
    public DateTime Date { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public User User1 { get; set; } = null!;
    public User User2 { get; set; } = null!;
}
=======
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
>>>>>>> 098b1416170f378db84d1e1b5fc6d1b0ca48244e
