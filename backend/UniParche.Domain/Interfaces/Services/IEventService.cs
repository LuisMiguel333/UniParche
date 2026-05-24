using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Services;

/// <summary>
/// Contrato del servicio de eventos/parches
/// </summary>
public interface IEventService
{
	// ═══ Consultas ═══
	Task<IEnumerable<Event>> GetAllEventsAsync();
	Task<Event?> GetEventByIdAsync(int eventId);
	Task<IEnumerable<Event>> GetEventsByUniversityAsync(int universityId);
	Task<IEnumerable<Event>> GetEventsByCreatorAsync(int creatorId);

	// ═══ Crear, Actualizar, Eliminar ═══
	Task<Event> CreateEventAsync(Event entity, int creatorId);
	Task<Event> UpdateEventAsync(Event entity);
	Task<bool> DeleteEventAsync(int eventId);
}