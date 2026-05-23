using Uniparches.Domain.Enums;

namespace UniParche.Domain.Entities;

public class Event
{
    public int Id { get; set; }
    public int CreatorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime EventDate { get; set; }
    public int Spots { get; set; }
    public string ImageUrl { get; set; }
    public EventStatus Status { get; set; } = EventStatus.Upcoming;
    public GroupType Type { get; set; }
    public int UniversityId { get; set; }
    public User Creator { get; set; }

    // navigation properties

    public University University { get; set; }
    public ICollection<EventAttendee> EventAttendees { get; set; } = new List<EventAttendee>();
}
