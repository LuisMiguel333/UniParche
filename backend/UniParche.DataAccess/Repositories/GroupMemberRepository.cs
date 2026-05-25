using Microsoft.EntityFrameworkCore;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Repositories;
using UniParche.DataAccess.DbContext;

namespace UniParche.DataAccess.Repositories;

public class GroupMemberRepository : GenericRepository<GroupMember>, IGroupMemberRepository
{
    public GroupMemberRepository(UniParcheDbContext context) : base(context)
    {
    }

    // ═══ Miembros por Grupo ═══

    /// <summary>
    /// Retorna todos los miembros de un grupo específico
    /// </summary>
    public async Task<IEnumerable<GroupMember>> GetByGroupAsync(int groupId)
    {
        return await _dbSet
            .Where(m => m.GroupId == groupId)
            .OrderByDescending(m => m.Id)
            .ToListAsync();
    }

    // ═══ Grupos por Usuario ═══

    /// <summary>
    /// Retorna todos los grupos a los que pertenece un usuario
    /// </summary>
    public async Task<IEnumerable<GroupMember>> GetByUserAsync(int userId)
    {
        return await _dbSet
            .Where(m => m.UserId == userId)
            .OrderByDescending(m => m.Id)
            .ToListAsync();
    }

    // ═══ Membresía Específica ═══

    /// <summary>
    /// Retorna la membresía de un usuario en un grupo específico.
    /// Retorna null si el usuario no es miembro.
    /// </summary>
    public async Task<GroupMember?> GetByGroupAndUserAsync(int groupId, int userId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(m => m.GroupId == groupId && m.UserId == userId);
    }
}