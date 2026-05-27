using UniParche.Domain.Enums;

namespace UniParche.Domain.Entities;

public class Event : AuditBase
{
    public int CreatorId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public int Capacity { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public EventStatus Status { get; set; } = EventStatus.Upcoming;
    public GroupType Type { get; set; }
    public int UniversityId { get; set; }

    // Navigation properties
    public User Creator { get; set; } = null!;
    public University University { get; set; } = null!;
    public ICollection<EventAttendee> EventAttendees { get; set; } = new List<EventAttendee>();
}
