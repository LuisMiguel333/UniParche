using UniParche.Domain.Enums;


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
        public Usuario User { get; set; }
    }
}