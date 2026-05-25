using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Repositories;
using UniParche.Domain.Interfaces.Services;

namespace UniParche.Domain.Services;

/// <summary>
/// Servicio que gestiona la lógica de negocio relacionada 
/// con los asistentes de un parche (evento).
/// </summary>
public class EventAttendeeService : IEventAttendeeService
{
	// ========== Dependencias ==========

	private readonly IEventAttendeeRepository _attendeeRepository;
	private readonly IEventRepository _eventRepository;

	public EventAttendeeService(
		IEventAttendeeRepository attendeeRepository,
		IEventRepository eventRepository)
	{
		_attendeeRepository = attendeeRepository;
		_eventRepository = eventRepository;
	}

	// ========== Consultas ==========

	/// <summary>
	/// Retorna todos los asistentes de un parche específico
	/// </summary>
	public async Task<IEnumerable<EventAttendee>> GetByEventAsync(int eventId)
		=> await _attendeeRepository.GetByEventAsync(eventId);

	/// <summary>
	/// Retorna todos los parches a los que se ha inscrito un usuario
	/// </summary>
	public async Task<IEnumerable<EventAttendee>> GetByUserAsync(int userId)
		=> await _attendeeRepository.GetByUserAsync(userId);

	// ========== Acciones ==========

	/// <summary>
	/// Inscribe a un usuario en un parche.
	/// Valida que el parche exista y que el usuario no esté ya inscrito.
	/// </summary>
	public async Task<EventAttendee> JoinEventAsync(int eventId, int userId)
	{
		// Verificar que el parche existe
		var eventExists = await _eventRepository.ExistsAsync(e => e.Id == eventId);
		if (!eventExists)
			throw new KeyNotFoundException($"El parche con ID {eventId} no existe.");

		// Verificar que el usuario no esté ya inscrito
		var alreadyJoined = await _attendeeRepository
			.ExistsAsync(a => a.EventId == eventId && a.UserId == userId);
		if (alreadyJoined)
			throw new InvalidOperationException("El usuario ya está inscrito en este parche.");

		// Crear la inscripción con estado pendiente por defecto
		var attendee = new EventAttendee
		{
			EventId = eventId,
			UserId = userId,
			Status = "Pending"
		};

		return await _attendeeRepository.AddAsync(attendee);
	}

	/// <summary>
	/// Actualiza el estado de asistencia de un usuario en un parche.
	/// Estados válidos: Pending, Confirmed, Declined.
	/// </summary>
	public async Task UpdateStatusAsync(int eventId, int userId, string status)
	{
		var attendee = await _attendeeRepository.GetByEventAndUserAsync(eventId, userId)
			?? throw new KeyNotFoundException("La inscripción no fue encontrada.");

		attendee.Status = status;
		await _attendeeRepository.UpdateAsync(attendee);
	}

	/// <summary>
	/// Elimina la inscripción de un usuario en un parche.
	/// Lanza excepción si la inscripción no existe.
	/// </summary>
	public async Task LeaveEventAsync(int eventId, int userId)
	{
		var attendee = await _attendeeRepository.GetByEventAndUserAsync(eventId, userId)
			?? throw new KeyNotFoundException("La inscripción no fue encontrada.");

		await _attendeeRepository.DeleteAsync(attendee);
	}
}