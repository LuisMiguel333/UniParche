namespace UniParche.API.DTOs.Response;

/// <summary>
/// DTO de respuesta para las inscripciones a un parche (evento)
/// </summary>
public class EventAttendeeResponse
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string EventTitle { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Estado de la inscripción: Pending, Confirmed, Declined
    /// </summary>
    public string Status { get; set; } = string.Empty;
}