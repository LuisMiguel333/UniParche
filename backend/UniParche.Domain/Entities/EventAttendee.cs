using UniParche.Domain.Enums;

<<<<<<< HEAD
namespace UniParche.Domain.Entities
{
    public class EventAttendee
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public AttendanceStatus Status { get; set; } = AttendanceStatus.Pending;
        // Navigation properties
        public Event Event { get; set; }
        public User User { get; set; }
    }
}
=======
namespace UniParche.Domain.Entities;

public class EventAttendee : AuditBase
{
    public int UserId { get; set; }
    public int EventId { get; set; }
    public AttendanceStatus Status { get; set; } = AttendanceStatus.Pending;

    // Navigation properties
    public Event Event { get; set; } = null!;
    public User User { get; set; } = null!;
}
>>>>>>> 098b1416170f378db84d1e1b5fc6d1b0ca48244e
