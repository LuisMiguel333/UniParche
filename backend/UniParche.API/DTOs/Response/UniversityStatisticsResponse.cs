namespace UniParche.API.DTOs.Response;

/// <summary>
/// DTO de respuesta con estadísticas de la universidad
/// </summary>
public class UniversityStatisticsResponse
{
    /// <summary>
    /// ID de la universidad
    /// </summary>
    public int UniversityId { get; set; }

    /// <summary>
    /// Nombre de la universidad
    /// </summary>
    public string UniversityName { get; set; } = string.Empty;

    /// <summary>
    /// Total de usuarios registrados
    /// </summary>
    public int TotalUsers { get; set; }

    /// <summary>
    /// Total de posts creados
    /// </summary>
    public int TotalPosts { get; set; }

    /// <summary>
    /// Total de comentarios
    /// </summary>
    public int TotalComments { get; set; }

    /// <summary>
    /// Total de likes
    /// </summary>
    public int TotalLikes { get; set; }

    /// <summary>
    /// Fecha de creación
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
