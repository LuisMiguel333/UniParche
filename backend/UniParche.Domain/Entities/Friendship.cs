using UniParche.Domain.Enums;

namespace UniParche.Domain.Entities;

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
