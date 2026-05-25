using Microsoft.EntityFrameworkCore;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Repositories;
using UniParche.DataAccess.DbContext;

namespace UniParche.DataAccess.Repositories;

public class FriendshipRepository : GenericRepository<Friendship>, IFriendshipRepository
{
    public FriendshipRepository(UniParcheDbContext context) : base(context)
    {
    }

    // ═══ Amistades por Usuario ═══

    /// <summary>
    /// Retorna todas las amistades de un usuario,
    /// tanto las enviadas como las recibidas
    /// </summary>
    public async Task<IEnumerable<Friendship>> GetByUserAsync(int userId)
    {
        return await _dbSet
            .Where(f => f.RequesterId == userId || f.AddresseeId == userId)
            .OrderByDescending(f => f.Id)
            .ToListAsync();
    }

    // ═══ Relación entre dos Usuarios ═══

    /// <summary>
    /// Retorna la relación de amistad entre dos usuarios específicos.
    /// Busca en ambas direcciones. Retorna null si no existe relación.
    /// </summary>
    public async Task<Friendship?> GetBetweenUsersAsync(int user1Id, int user2Id)
    {
        return await _dbSet
            .FirstOrDefaultAsync(f =>
                (f.RequesterId == user1Id && f.AddresseeId == user2Id) ||
                (f.RequesterId == user2Id && f.AddresseeId == user1Id));
    }
}