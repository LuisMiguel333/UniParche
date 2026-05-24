using UniParche.Domain.Enums;

namespace UniParche.API.DTOs.Response;

/// <summary>
/// DTO de respuesta para información de un parche
/// </summary>
public class EventResponse
{
	/// <summary>
	/// ID del parche
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Título del parche
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// Descripción del parche
	/// </summary>
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// Lugar donde se realiza el parche
	/// </summary>
	public string Location { get; set; } = string.Empty;

	/// <summary>
	/// Fecha y hora del parche
	/// </summary>
	public DateTime EventDate { get; set; }

	/// <summary>
	/// Cupos disponibles
	/// </summary>
	public int Capacity { get; set; }

	/// <summary>
	/// URL de la imagen del parche
	/// </summary>
	public string? ImageUrl { get; set; }

	/// <summary>
	/// Estado del parche (Upcoming, Active, Cancelled, Finished)
	/// </summary>
	public EventStatus Status { get; set; }

	/// <summary>
	/// ID del usuario que creó el parche
	/// </summary>
	public int CreatorId { get; set; }

	/// <summary>
	/// Nombre del creador
	/// </summary>
	public string CreatorName { get; set; } = string.Empty;

	/// <summary>
	/// ID de la universidad
	/// </summary>
	public int UniversityId { get; set; }

	/// <summary>
	/// Cuántas personas ya se unieron al parche
	/// </summary>
	public int AttendeeCount { get; set; }

	/// <summary>
	/// Fecha de creación del registro
	/// </summary>
	public DateTime CreatedAt { get; set; }
}