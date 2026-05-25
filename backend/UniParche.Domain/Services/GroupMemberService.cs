using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Repositories;
using UniParche.Domain.Interfaces.Services;

namespace UniParche.Domain.Services;

/// <summary>
/// Servicio que gestiona la lógica de negocio relacionada
/// con los miembros de un grupo universitario.
/// </summary>
public class GroupMemberService : IGroupMemberService
{
    // ========== Dependencias ==========

    private readonly IGroupMemberRepository _memberRepository;
    private readonly IGroupRepository _groupRepository;

    public GroupMemberService(
        IGroupMemberRepository memberRepository,
        IGroupRepository groupRepository)
    {
        _memberRepository = memberRepository;
        _groupRepository = groupRepository;
    }

    // ========== Consultas ==========

    /// <summary>
    /// Retorna todos los miembros de un grupo específico
    /// </summary>
    public async Task<IEnumerable<GroupMember>> GetByGroupAsync(int groupId)
        => await _memberRepository.GetByGroupAsync(groupId);

    /// <summary>
    /// Retorna todos los grupos a los que pertenece un usuario
    /// </summary>
    public async Task<IEnumerable<GroupMember>> GetByUserAsync(int userId)
        => await _memberRepository.GetByUserAsync(userId);

    // ========== Acciones ==========

    /// <summary>
    /// Agrega un usuario como miembro de un grupo.
    /// Valida que el grupo exista y que el usuario no sea ya miembro.
    /// </summary>
    public async Task<GroupMember> JoinGroupAsync(int groupId, int userId)
    {
        // Verificar que el grupo existe
        var groupExists = await _groupRepository.ExistsAsync(g => g.Id == groupId);
        if (!groupExists)
            throw new KeyNotFoundException($"El grupo con ID {groupId} no existe.");

        // Verificar que el usuario no sea ya miembro
        var alreadyMember = await _memberRepository
            .ExistsAsync(m => m.GroupId == groupId && m.UserId == userId);
        if (alreadyMember)
            throw new InvalidOperationException("El usuario ya es miembro de este grupo.");

        // Crear la membresía con rol básico por defecto
        var member = new GroupMember
        {
            GroupId = groupId,
            UserId = userId,
            Role = "Member"
        };

        return await _memberRepository.AddAsync(member);
    }

    /// <summary>
    /// Actualiza el rol de un miembro dentro del grupo.
    /// Roles válidos: Member, Moderator, Admin.
    /// </summary>
    public async Task UpdateRoleAsync(int groupId, int userId, string role)
    {
        var member = await _memberRepository.GetByGroupAndUserAsync(groupId, userId)
            ?? throw new KeyNotFoundException("El miembro no fue encontrado en este grupo.");

        member.Role = role;
        await _memberRepository.UpdateAsync(member);
    }

    /// <summary>
    /// Elimina la membresía de un usuario en un grupo.
    /// Lanza excepción si el usuario no es miembro.
    /// </summary>
    public async Task LeaveGroupAsync(int groupId, int userId)
    {
        var member = await _memberRepository.GetByGroupAndUserAsync(groupId, userId)
            ?? throw new KeyNotFoundException("El miembro no fue encontrado en este grupo.");

        await _memberRepository.DeleteAsync(member);
    }
}