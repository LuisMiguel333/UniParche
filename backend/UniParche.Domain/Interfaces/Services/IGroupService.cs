using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Services;

/// <summary>
/// Contrato del servicio de grupos de estudio
/// </summary>
public interface IGroupService
{
    // ═══ Consultas ═══
    Task<IEnumerable<Group>> GetAllAsync();
    Task<Group?> GetByIdAsync(int groupId);
    Task<IEnumerable<Group>> GetByUniversityAsync(int universityId);
    Task<IEnumerable<Group>> GetByCreatorAsync(int creatorId);

    // ═══ Crear, Actualizar, Eliminar ═══
    Task<Group> CreateAsync(Group entity);
    Task<Group> UpdateAsync(int id, Group entity);
    Task<bool> DeleteAsync(int groupId);
}