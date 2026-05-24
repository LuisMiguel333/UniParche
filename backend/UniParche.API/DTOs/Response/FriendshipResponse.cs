using UniParche.Domain.Enums;

namespace UniParche.API.DTOs.Response;

/// <summary>
/// DTO de respuesta para información de una amistad
/// </summary>
public class FriendshipResponse
{
	/// <summary>
	/// ID del usuario que envió la solicitud
	/// </summary>
	public int User1Id { get; set; }

	/// <summary>
	/// Nombre del usuario que envió la solicitud
	/// </summary>
	public string User1Name { get; set; } = string.Empty;

	/// <summary>
	/// ID del usuario que recibió la solicitud
	/// </summary>
	public int User2Id { get; set; }

	/// <summary>
	/// Nombre del usuario que recibió la solicitud
	/// </summary>
	public string User2Name { get; set; } = string.Empty;

	/// <summary>
	/// Estado de la amistad (Pending, Accepted, Rejected)
	/// </summary>
	public FriendshipStatus Status { get; set; }

	/// <summary>
	/// Fecha en que se envió la solicitud
	/// </summary>
	public DateTime Date { get; set; }
}