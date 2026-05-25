using Microsoft.EntityFrameworkCore;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Repositories;
using UniParche.DataAccess.DbContext;

namespace UniParche.DataAccess.Repositories;

public class EventAttendeeRepository : GenericRepository<EventAttendee>, IEventAttendeeRepository
{
    public EventAttendeeRepository(UniParcheDbContext context) : base(context)
    {
    }

    // ═══ Asistentes por Parche ═══

    /// <summary>
    /// Retorna todos los asistentes de un parche específico
    /// </summary>
    public async Task<IEnumerable<EventAttendee>> GetByEventAsync(int eventId)
    {
        return await _dbSet
            .Where(a => a.EventId == eventId)
            .OrderByDescending(a => a.Id)
            .ToListAsync();
    }

    // ═══ Parches por Usuario ═══

    /// <summary>
    /// Retorna todas las inscripciones de un usuario específico
    /// </summary>
    public async Task<IEnumerable<EventAttendee>> GetByUserAsync(int userId)
    {
        return await _dbSet
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.Id)
            .ToListAsync();
    }

    // ═══ Inscripción Específica ═══

    /// <summary>
    /// Retorna la inscripción de un usuario en un parche específico.
    /// Retorna null si no existe.
    /// </summary>
    public async Task<EventAttendee?> GetByEventAndUserAsync(int eventId, int userId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(a => a.EventId == eventId && a.UserId == userId);
    }
}