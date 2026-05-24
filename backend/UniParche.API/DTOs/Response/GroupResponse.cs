using UniParche.Domain.Enums;

namespace UniParche.API.DTOs.Response;

/// <summary>
/// DTO de respuesta para información de un grupo
/// </summary>
public class GroupResponse
{
    /// <summary>
    /// ID del grupo
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre del grupo
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del grupo
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Materia del grupo
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de grupo (Study, Social, etc.)
    /// </summary>
    public GroupType Type { get; set; }

    /// <summary>
    /// ID del creador
    /// </summary>
    public int CreatorId { get; set; }

    /// <summary>
    /// Nombre del creador
    /// </summary>
    public string CreatorName { get; set; } = string.Empty;

    /// <summary>
    /// ID de la universidad
    /// </summary>
    public int UniversityId { get; set; }

    /// <summary>
    /// Cuántos miembros tiene el grupo
    /// </summary>
    public int MemberCount { get; set; }

    /// <summary>
    /// Fecha de creación
    /// </summary>
    public DateTime CreatedAt { get; set; }
}