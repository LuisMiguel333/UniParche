namespace UniParche.API.DTOs.Response;

using UniParche.Domain.Enums;

/// <summary>
/// DTO de respuesta para información de un like
/// </summary>
public class LikeResponse
{
    /// <summary>
    /// ID del like
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID del usuario que dio el like
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Nombre del usuario que dio el like
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// ID del post que recibió el like
    /// </summary>
    public int PostId { get; set; }

    /// <summary>
    /// Tipo de reacción (Like, Love, Sad, Angry, Wow, Haha, Care)
    /// </summary>
    public ReactionType ReactionType { get; set; } = ReactionType.Like;
}
