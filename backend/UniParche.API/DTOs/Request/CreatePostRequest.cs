namespace UniParche.API.DTOs.Request;

/// <summary>
/// DTO para crear un nuevo post
/// </summary>
public class CreatePostRequest
{
    /// <summary>
    /// Título del post
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Contenido del post
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// URL de la imagen (opcional)
    /// </summary>
    public string? ImageUrl { get; set; }
}
