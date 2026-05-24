using Microsoft.Extensions.Logging;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Repositories;
using UniParche.Domain.Interfaces.Services;

namespace UniParche.Domain.Services;

/// <summary>
/// Implementación del servicio de grupos de estudio
/// </summary>
public class GroupService : IGroupService
{
    private readonly IGenericRepository<Group> _groupRepository;
    private readonly ILogger<GroupService> _logger;

    public GroupService(
        IGenericRepository<Group> groupRepository,
        ILogger<GroupService> logger)
    {
        _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    // ═══ Consultas ═══

    public async Task<IEnumerable<Group>> GetAllGroupsAsync()
    {
        _logger.LogInformation("Obteniendo todos los grupos");
        return await _groupRepository.GetAllAsync();
    }

    public async Task<Group?> GetGroupByIdAsync(int groupId)
    {
        if (groupId <= 0) return null;

        _logger.LogInformation("Obteniendo grupo con ID {GroupId}", groupId);
        return await _groupRepository.GetByIdAsync(groupId);
    }

    public async Task<IEnumerable<Group>> GetGroupsByUniversityAsync(int universityId)
    {
        if (universityId <= 0) return Enumerable.Empty<Group>();

        _logger.LogInformation("Obteniendo grupos de la universidad {UniversityId}", universityId);
        return await _groupRepository.GetByExpressionAsync(g => g.UniversityId == universityId);
    }

    public async Task<IEnumerable<Group>> GetGroupsByCreatorAsync(int creatorId)
    {
        if (creatorId <= 0) return Enumerable.Empty<Group>();

        _logger.LogInformation("Obteniendo grupos del creador {CreatorId}", creatorId);
        return await _groupRepository.GetByExpressionAsync(g => g.CreatorId == creatorId);
    }

    // ═══ Crear, Actualizar, Eliminar ═══

    public async Task<Group> CreateGroupAsync(Group entity, int creatorId)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        if (string.IsNullOrWhiteSpace(entity.Name))
            throw new ArgumentException("El nombre del grupo es obligatorio.");
        if (string.IsNullOrWhiteSpace(entity.Subject))
            throw new ArgumentException("La materia del grupo es obligatoria.");

        entity.CreatorId = creatorId;
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;

        _logger.LogInformation("Creando nuevo grupo: {Name}", entity.Name);
        return await _groupRepository.AddAsync(entity);
    }

    public async Task<Group> UpdateGroupAsync(Group entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        if (entity.Id <= 0)
            throw new ArgumentException("El grupo debe tener un ID válido.");
        if (string.IsNullOrWhiteSpace(entity.Name))
            throw new ArgumentException("El nombre del grupo es obligatorio.");

        var existing = await _groupRepository.GetByIdAsync(entity.Id)
            ?? throw new KeyNotFoundException($"No se encontró el grupo con ID {entity.Id}.");

        existing.Name = entity.Name;
        existing.Description = entity.Description;
        existing.Subject = entity.Subject;
        existing.Type = entity.Type;
        existing.UpdatedAt = DateTime.UtcNow;

        _logger.LogInformation("Actualizando grupo: {GroupId}", entity.Id);
        return await _groupRepository.UpdateAsync(existing);
    }

    public async Task<bool> DeleteGroupAsync(int groupId)
    {
        var existing = await _groupRepository.GetByIdAsync(groupId)
            ?? throw new KeyNotFoundException($"No se encontró el grupo con ID {groupId}.");

        _logger.LogInformation("Eliminando grupo: {GroupId}", groupId);
        return await _groupRepository.DeleteAsync(groupId);
    }
}