using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Repositories;

public interface IEventAttendeeRepository : IGenericRepository<EventAttendee>
{
    // ========== Consultas Específicas de Asistentes ==========

    /// <summary>
    /// Obtiene todos los asistentes de un parche específico
    /// </summary>
    Task<IEnumerable<EventAttendee>> GetByEventAsync(int eventId);

    /// <summary>
    /// Obtiene todos los parches a los que asiste un usuario específico
    /// </summary>
    Task<IEnumerable<EventAttendee>> GetByUserAsync(int userId);

    /// <summary>
    /// Obtiene la asistencia de un usuario en un parche específico.
    /// Retorna null si el usuario no está en el parche.
    /// </summary>
    Task<EventAttendee?> GetByEventAndUserAsync(int eventId, int userId);
}