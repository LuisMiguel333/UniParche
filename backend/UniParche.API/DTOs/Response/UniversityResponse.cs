namespace UniParche.API.DTOs.Response;

/// <summary>
/// DTO de respuesta para información de la universidad
/// </summary>
public class UniversityResponse
{
    /// <summary>
    /// ID de la universidad
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre de la universidad
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Email de dominio de la universidad
    /// </summary>
    public string DomainEmail { get; set; } = string.Empty;

    /// <summary>
    /// Fecha de creación
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Última fecha de actualización
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
