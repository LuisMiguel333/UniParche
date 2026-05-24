namespace UniParche.API.DTOs.Request;

/// <summary>
/// DTO para crear un nuevo comentario
/// </summary>
public class CreateCommentRequest
{
    /// <summary>
    /// ID del post
    /// </summary>
    public int PostId { get; set; }

    /// <summary>
    /// Contenido del comentario
    /// </summary>
    public string Content { get; set; } = string.Empty;
}
