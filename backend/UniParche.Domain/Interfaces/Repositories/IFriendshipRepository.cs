using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Repositories;

public interface IFriendshipRepository : IGenericRepository<Friendship>
{
    // ========== Consultas Específicas de Amistades ==========

    /// <summary>
    /// Obtiene todas las amistades de un usuario,
    /// tanto las enviadas como las recibidas
    /// </summary>
    Task<IEnumerable<Friendship>> GetByUserAsync(int userId);

    /// <summary>
    /// Obtiene la relación de amistad entre dos usuarios específicos.
    /// Retorna null si no existe relación entre ellos.
    /// </summary>
    Task<Friendship?> GetBetweenUsersAsync(int user1Id, int user2Id);
}