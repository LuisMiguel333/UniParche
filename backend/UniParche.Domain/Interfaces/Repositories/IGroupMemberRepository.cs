using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Repositories;

public interface IGroupMemberRepository : IGenericRepository<GroupMember>
{
    // ========== Consultas Específicas de Miembros ==========

    /// <summary>
    /// Obtiene todos los miembros de un grupo específico
    /// </summary>
    Task<IEnumerable<GroupMember>> GetByGroupAsync(int groupId);

    /// <summary>
    /// Obtiene todos los grupos a los que pertenece un usuario específico
    /// </summary>
    Task<IEnumerable<GroupMember>> GetByUserAsync(int userId);

    /// <summary>
    /// Obtiene la membresía de un usuario en un grupo específico.
    /// Retorna null si el usuario no es miembro del grupo.
    /// </summary>
    Task<GroupMember?> GetByGroupAndUserAsync(int groupId, int userId);
}