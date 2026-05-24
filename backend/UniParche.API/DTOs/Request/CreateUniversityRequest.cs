namespace UniParche.API.DTOs.Request;

/// <summary>
/// DTO para crear una nueva universidad
/// </summary>
public class CreateUniversityRequest
{
    /// <summary>
    /// Nombre de la universidad
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Email de dominio de la universidad
    /// </summary>
    public string DomainEmail { get; set; } = string.Empty;
}
