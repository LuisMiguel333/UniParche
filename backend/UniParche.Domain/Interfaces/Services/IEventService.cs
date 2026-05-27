using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Services;

/// <summary>
/// Contrato del servicio de eventos/parches
/// </summary>
public interface IEventService
{
	// ═══ Consultas ═══
	Task<IEnumerable<Event>> GetAllAsync();
	Task<Event?> GetByIdAsync(int eventId);
	Task<IEnumerable<Event>> GetByUniversityAsync(int universityId);
	Task<IEnumerable<Event>> GetByCreatorAsync(int creatorId);

	// ═══ Crear, Actualizar, Eliminar ═══
	Task<Event> CreateAsync(Event entity);
	Task<Event> UpdateAsync(int id, Event entity);
	Task<bool> DeleteAsync(int eventId);
}