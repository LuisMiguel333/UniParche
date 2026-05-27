using Microsoft.Extensions.Logging;
using UniParche.Domain.Entities;
using UniParche.Domain.Enums;
using UniParche.Domain.Interfaces.Repositories;
using UniParche.Domain.Interfaces.Services;

namespace UniParche.Domain.Services;

/// <summary>
/// Implementación del servicio de parches/eventos
/// </summary>
public class EventService : IEventService
{
	private readonly IGenericRepository<Event> _eventRepository;
	private readonly ILogger<EventService> _logger;

	public EventService(
		IGenericRepository<Event> eventRepository,
		ILogger<EventService> logger)
	{
		_eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	// ═══ Consultas ═══

	public async Task<IEnumerable<Event>> GetAllAsync()
	{
		_logger.LogInformation("Obteniendo todos los parches");
		return await _eventRepository.GetAllAsync();
	}

	public async Task<Event?> GetByIdAsync(int eventId)
	{
		if (eventId <= 0) return null;

		_logger.LogInformation("Obteniendo parche con ID {EventId}", eventId);
		return await _eventRepository.GetByIdAsync(eventId);
	}

	public async Task<IEnumerable<Event>> GetByUniversityAsync(int universityId)
	{
		if (universityId <= 0) return Enumerable.Empty<Event>();

		_logger.LogInformation("Obteniendo parches de la universidad {UniversityId}", universityId);
		return await _eventRepository.GetByExpressionAsync(e => e.UniversityId == universityId);
	}

	public async Task<IEnumerable<Event>> GetByCreatorAsync(int creatorId)
	{
		if (creatorId <= 0) return Enumerable.Empty<Event>();

		_logger.LogInformation("Obteniendo parches del creador {CreatorId}", creatorId);
		return await _eventRepository.GetByExpressionAsync(e => e.CreatorId == creatorId);
	}

	// ═══ Crear, Actualizar, Eliminar ═══

	public async Task<Event> CreateAsync(Event entity)
	{
		if (entity == null)
			throw new ArgumentNullException(nameof(entity));
		if (string.IsNullOrWhiteSpace(entity.Title))
			throw new ArgumentException("El título del parche es obligatorio.");
		if (entity.EventDate < DateTime.UtcNow)
			throw new ArgumentException("La fecha del parche no puede ser en el pasado.");
		if (entity.Capacity <= 0)
			throw new ArgumentException("Los cupos deben ser mayor a 0.");

		entity.Status = EventStatus.Upcoming;
		entity.CreatedAt = DateTime.UtcNow;
		entity.UpdatedAt = DateTime.UtcNow;

		_logger.LogInformation("Creando nuevo parche: {Title}", entity.Title);
		return await _eventRepository.AddAsync(entity);
	}

	public async Task<Event> UpdateAsync(int id, Event entity)
	{
		if (entity == null)
			throw new ArgumentNullException(nameof(entity));
		if (id <= 0)
			throw new ArgumentException("El ID debe ser válido.");
		if (string.IsNullOrWhiteSpace(entity.Title))
			throw new ArgumentException("El título del parche es obligatorio.");

		var existing = await _eventRepository.GetByIdAsync(id)
			?? throw new KeyNotFoundException($"No se encontró el parche con ID {id}.");

		existing.Title = entity.Title;
		existing.Description = entity.Description;
		existing.Location = entity.Location;
		existing.EventDate = entity.EventDate;
		existing.Capacity = entity.Capacity;
		existing.ImageUrl = entity.ImageUrl;
		existing.Status = entity.Status;
		existing.UpdatedAt = DateTime.UtcNow;

		_logger.LogInformation("Actualizando parche: {EventId}", id);
		return await _eventRepository.UpdateAsync(existing);
	}

	public async Task<bool> DeleteAsync(int eventId)
	{
		var existing = await _eventRepository.GetByIdAsync(eventId)
			?? throw new KeyNotFoundException($"No se encontró el parche con ID {eventId}.");

		_logger.LogInformation("Eliminando parche: {EventId}", eventId);
		return await _eventRepository.DeleteAsync(eventId);
	}
}