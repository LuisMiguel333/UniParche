using Microsoft.EntityFrameworkCore;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Repositories;
using UniParche.DataAccess.DbContext;

namespace UniParche.DataAccess.Repositories;

public class GroupRepository : GenericRepository<Group>, IGroupRepository
{
    public GroupRepository(UniParcheDbContext context) : base(context)
    {
    }

    // ═══ Grupos por Universidad ═══

    /// <summary>
    /// Retorna todos los grupos de una universidad específica
    /// </summary>
    public async Task<IEnumerable<Group>> GetByUniversityAsync(int universityId)
    {
        return await _dbSet
            .Where(g => g.UniversityId == universityId)
            .OrderByDescending(g => g.Id)
            .ToListAsync();
    }

    // ═══ Grupo con Miembros ═══

    /// <summary>
    /// Retorna un grupo incluyendo su lista completa de miembros
    /// </summary>
    public async Task<Group?> GetWithMembersAsync(int id)
    {
        return await _dbSet
            .Include(g => g.Members)
            .FirstOrDefaultAsync(g => g.Id == id);
    }
}