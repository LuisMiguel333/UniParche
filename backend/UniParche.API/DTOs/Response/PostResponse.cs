namespace UniParche.API.DTOs.Response;

/// <summary>
/// DTO de respuesta para información de un post
/// </summary>
public class PostResponse
{
    /// <summary>
    /// ID del post
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Título del post
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Contenido del post
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// URL de la imagen
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// ID del usuario creador
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Nombre del usuario creador
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// ID de la universidad
    /// </summary>
    public int UniversityId { get; set; }

    /// <summary>
    /// Fecha de creación
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Número de comentarios
    /// </summary>
    public int CommentCount { get; set; }

    /// <summary>
    /// Número de likes
    /// </summary>
    public int LikeCount { get; set; }

    /// <summary>
    /// Si el usuario actual ha dado like (cuando aplique)
    /// </summary>
    public bool IsLikedByCurrentUser { get; set; }
}
