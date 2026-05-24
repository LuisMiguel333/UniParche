using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Services;

/// <summary>
/// Contrato del servicio de grupos de estudio
/// </summary>
public interface IGroupService
{
    // ═══ Consultas ═══
    Task<IEnumerable<Group>> GetAllGroupsAsync();
    Task<Group?> GetGroupByIdAsync(int groupId);
    Task<IEnumerable<Group>> GetGroupsByUniversityAsync(int universityId);
    Task<IEnumerable<Group>> GetGroupsByCreatorAsync(int creatorId);

    // ═══ Crear, Actualizar, Eliminar ═══
    Task<Group> CreateGroupAsync(Group entity, int creatorId);
    Task<Group> UpdateGroupAsync(Group entity);
    Task<bool> DeleteGroupAsync(int groupId);
}