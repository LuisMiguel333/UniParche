using UniParche.Domain.Enums;

namespace UniParche.Domain.Entities
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