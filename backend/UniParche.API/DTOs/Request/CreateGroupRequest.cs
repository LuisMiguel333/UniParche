using System.ComponentModel.DataAnnotations;
using UniParche.Domain.Enums;

namespace UniParche.API.DTOs.Request;

/// <summary>
///  DTO para crear un grupo, se necesita de manera obligatoria nombre, descripcion, materia (Subject), universidadId y creatorId por defecto el tipo de grupo es de estudio 
public class CreateGroupRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Subject { get; set; } = string.Empty;

    [Required]
    public int UniversityId { get; set; }

    [Required]
    public int CreatorId { get; set; }

    public GroupType Type { get; set; } = GroupType.Study;
}