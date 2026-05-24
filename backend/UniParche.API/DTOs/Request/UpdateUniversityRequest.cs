namespace UniParche.API.DTOs.Request;

/// <summary>
/// DTO para actualizar información de una universidad
/// </summary>
public class UpdateUniversityRequest
{
    /// <summary>
    /// Nuevo nombre de la universidad
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Nuevo email de dominio
    /// </summary>
    public string? DomainEmail { get; set; }
}
