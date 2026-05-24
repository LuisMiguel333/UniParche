<<<<<<< HEAD
<<<<<<< HEAD
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
=======
=======
>>>>>>> 098b1416170f378db84d1e1b5fc6d1b0ca48244e
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
<<<<<<< HEAD
}
>>>>>>> 098b1416170f378db84d1e1b5fc6d1b0ca48244e
=======
}
>>>>>>> 098b1416170f378db84d1e1b5fc6d1b0ca48244e
