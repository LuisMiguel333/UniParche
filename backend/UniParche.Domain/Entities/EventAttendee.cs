using UniParche.Domain.Enums;

namespace UniParche.Domain.Entities;

public class EventAttendee : AuditBase
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int EventId { get; set; }
    public string Status { get; set; } = "Pending";

    // Navigation properties
    public Event Event { get; set; } = null!;
    public User User { get; set; } = null!;
}
