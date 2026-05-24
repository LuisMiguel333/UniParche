namespace UniParche.API.DTOs.Response;

/// <summary>
/// DTO de respuesta para información de un comentario
/// </summary>
public class CommentResponse
{
    /// <summary>
    /// ID del comentario
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Contenido del comentario
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// ID del usuario que realizó el comentario
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Nombre del usuario que realizó el comentario
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// URL de la foto de perfil del usuario
    /// </summary>
    public string UserProfilePictureUrl { get; set; } = string.Empty;

    /// <summary>
    /// ID del post comentado
    /// </summary>
    public int PostId { get; set; }

    /// <summary>
    /// Fecha de creación del comentario
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Fecha de última actualización
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
