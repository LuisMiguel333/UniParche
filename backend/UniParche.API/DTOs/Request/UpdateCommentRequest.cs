namespace UniParche.API.DTOs.Request;

/// <summary>
/// DTO para actualizar un comentario
/// </summary>
public class UpdateCommentRequest
{
    /// <summary>
    /// Nuevo contenido del comentario
    /// </summary>
    public string Content { get; set; } = string.Empty;
}
