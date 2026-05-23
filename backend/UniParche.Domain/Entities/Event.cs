using UniParche.Domain.Enums;

namespace UniParche.Domain.Entities

    public class Event
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime EventDate { get; set; }
        public int Capacity { get; set; }
        public string ImageUrl { get; set; }
        public EventStatus Status { get; set; } = EventStatus.Upcoming;
        public GroupType Type { get; set; }
        public int UniversityId { get; set; }
        public Usuario Creator { get; set; }


        // navigation properties

        public Universidad University { get; set; }
        public ICollection<EventAttendee> EventAttendees { get; set; } = new List<EventAttendee>();

    }


       