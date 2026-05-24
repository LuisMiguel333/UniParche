using UniParche.Domain.Enums;

<<<<<<< HEAD
<<<<<<< HEAD
namespace UniParche.Domain.Entities
{
    public class GroupMember
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public GroupRole Role { get; set; } = GroupRole.Member;
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;

        // Navigation properties

        public Group Group { get; set; }
        public User User { get; set; }
    }
}
=======
=======
>>>>>>> 098b1416170f378db84d1e1b5fc6d1b0ca48244e
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
<<<<<<< HEAD
}
>>>>>>> 098b1416170f378db84d1e1b5fc6d1b0ca48244e
=======
}
>>>>>>> 098b1416170f378db84d1e1b5fc6d1b0ca48244e
