using System.ComponentModel.DataAnnotations;
using UniParche.Domain.Enums;

namespace UniParche.API.DTOs.Request;

/// <summary>
/// DTO para editar un grupo, se necesita de manera obligatoria nombre, descripcion, materia (Subject) y elk typo de grupo si el usario asi lo quiere
/// 
public class UpdateGroupRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Subject { get; set; } = string.Empty;

    public GroupType Type { get; set; }
}