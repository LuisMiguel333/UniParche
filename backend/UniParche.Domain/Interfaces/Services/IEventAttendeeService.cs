using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Services;

public interface IEventAttendeeService
{
	// ========== Consultas ==========

	/// <summary>
	/// Obtiene todos los asistentes de un parche específico
	/// </summary>
	Task<IEnumerable<EventAttendee>> GetByEventAsync(int eventId);

	/// <summary>
	/// Obtiene todos los parches a los que asiste un usuario específico
	/// </summary>
	Task<IEnumerable<EventAttendee>> GetByUserAsync(int userId);

	// ========== Acciones ==========

	/// <summary>
	/// Registra a un usuario como asistente de un parche.
	/// Valida que haya cupos disponibles y que no esté ya inscrito.
	/// </summary>
	Task<EventAttendee> JoinEventAsync(int eventId, int userId);

	/// <summary>
	/// Actualiza el estado de asistencia de un usuario en un parche
	/// (Pending, Confirmed, Declined)
	/// </summary>
	Task UpdateStatusAsync(int eventId, int userId, string status);

	/// <summary>
	/// Elimina la inscripción de un usuario en un parche
	/// </summary>
	Task LeaveEventAsync(int eventId, int userId);
}