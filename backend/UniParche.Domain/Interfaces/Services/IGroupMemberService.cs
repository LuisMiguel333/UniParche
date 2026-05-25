using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Services;

public interface IGroupMemberService
{
    // ========== Consultas ==========

    /// <summary>
    /// Obtiene todos los miembros de un grupo específico
    /// </summary>
    Task<IEnumerable<GroupMember>> GetByGroupAsync(int groupId);

    /// <summary>
    /// Obtiene todos los grupos a los que pertenece un usuario específico
    /// </summary>
    Task<IEnumerable<GroupMember>> GetByUserAsync(int userId);

    // ========== Acciones ==========

    /// <summary>
    /// Agrega un usuario como miembro de un grupo.
    /// Valida que el grupo exista y que el usuario no sea ya miembro.
    /// </summary>
    Task<GroupMember> JoinGroupAsync(int groupId, int userId);

    /// <summary>
    /// Actualiza el rol de un miembro dentro de un grupo
    /// (Member, Moderator, Admin)
    /// </summary>
    Task UpdateRoleAsync(int groupId, int userId, string role);

    /// <summary>
    /// Elimina la membresía de un usuario en un grupo
    /// </summary>
    Task LeaveGroupAsync(int groupId, int userId);
}