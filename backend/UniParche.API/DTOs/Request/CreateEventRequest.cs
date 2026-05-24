
using System.ComponentModel.DataAnnotations;
using UniParche.Domain.Enums;

namespace UniParche.API.DTOs.Request;

/// <summary>
/// DTO para crear evento se necesita de manera obligatoria  titulo , descripcion, lugar, fecha, capacidad, universidadId y creatorId
/// </summary>

public class CreateEventRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Location { get; set; } = string.Empty;

    [Required]
    public DateTime EventDate { get; set; }

    [Range(1, 500)]
    public int Capacity { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    [Required]
    public int UniversityId { get; set; }

    [Required]
    public int CreatorId { get; set; }
}