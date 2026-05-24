namespace UniParche.API.DTOs.Request;

/// <summary>
/// DTO para actualizar un post
/// </summary>
public class UpdatePostRequest
{
    /// <summary>
    /// Nuevo título del post
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Nuevo contenido del post
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Nueva URL de la imagen
    /// </summary>
    public string? ImageUrl { get; set; }
}
