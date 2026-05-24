using UniParche.Domain.Enums;

<<<<<<< HEAD
<<<<<<< HEAD
namespace Uniparche.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UniversityId { get; set; }
        public string Subject { get; set; }
        public int CreatorId { get; set; }
        public GroupType Type { get; set; } = GroupType.Study;
        // Navigation properties
        public University University { get; set; }
        public User Creator { get; set; }
        public ICollection<GroupMember> Members { get; set; } = new List<GroupMember>();
    }
}
=======
=======
>>>>>>> 098b1416170f378db84d1e1b5fc6d1b0ca48244e
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
<<<<<<< HEAD
}
>>>>>>> 098b1416170f378db84d1e1b5fc6d1b0ca48244e
=======
}
>>>>>>> 098b1416170f378db84d1e1b5fc6d1b0ca48244e
