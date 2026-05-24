namespace UniParche.API.DTOs.Response;

/// <summary>
/// DTO de respuesta con estadísticas del usuario
/// </summary>
public class UserStatisticsResponse
{
    /// <summary>
    /// ID del usuario
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Nombre de usuario
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Total de posts creados
    /// </summary>
    public int TotalPosts { get; set; }

    /// <summary>
    /// Total de comentarios realizados
    /// </summary>
    public int TotalComments { get; set; }

    /// <summary>
    /// Total de likes dados
    /// </summary>
    public int TotalLikesGiven { get; set; }

    /// <summary>
    /// Total de likes recibidos
    /// </summary>
    public int TotalLikesReceived { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime RegisterDate { get; set; }
}
