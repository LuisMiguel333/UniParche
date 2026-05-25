using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Repositories;

public interface IEventRepository : IGenericRepository<Event>
{
    // ========== Consultas Específicas de Parches ==========

    /// <summary>
    /// Obtiene todos los parches de una universidad específica
    /// </summary>
    Task<IEnumerable<Event>> GetByUniversityAsync(int universityId);

    /// <summary>
    /// Obtiene todos los parches creados por un usuario específico
    /// </summary>
    Task<IEnumerable<Event>> GetByCreatorAsync(int creatorId);

    /// <summary>
    /// Obtiene un parche con su lista completa de asistentes incluida
    /// </summary>
    Task<Event?> GetWithAttendeesAsync(int id);
}