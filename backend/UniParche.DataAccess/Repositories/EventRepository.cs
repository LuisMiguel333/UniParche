using Microsoft.EntityFrameworkCore;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Repositories;
using UniParche.DataAccess.DbContext;

namespace UniParche.DataAccess.Repositories;

public class EventRepository : GenericRepository<Event>, IEventRepository
{
	public EventRepository(UniParcheDbContext context) : base(context)
	{
	}

	// ═══ Parches por Universidad ═══

	/// <summary>
	/// Retorna todos los parches de una universidad específica
	/// ordenados por fecha de creación descendente
	/// </summary>
	public async Task<IEnumerable<Event>> GetByUniversityAsync(int universityId)
	{
		return await _dbSet
			.Where(e => e.UniversityId == universityId)
			.OrderByDescending(e => e.Id)
			.ToListAsync();
	}

	// ═══ Parches por Creador ═══

	/// <summary>
	/// Retorna todos los parches creados por un usuario específico
	/// </summary>
	public async Task<IEnumerable<Event>> GetByCreatorAsync(int creatorId)
	{
		return await _dbSet
			.Where(e => e.CreatorId == creatorId)
			.OrderByDescending(e => e.Id)
			.ToListAsync();
	}

	// ═══ Parche con Asistentes ═══

	/// <summary>
	/// Retorna un parche incluyendo su lista completa de asistentes
	/// </summary>
	public async Task<Event?> GetWithAttendeesAsync(int id)
	{
		return await _dbSet
			.Include(e => e.Attendees)
			.FirstOrDefaultAsync(e => e.Id == id);
	}
}