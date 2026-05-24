namespace UniParche.API.DTOs.Request;

using UniParche.Domain.Enums;

/// <summary>
/// DTO para crear/dar un like
/// </summary>
public class CreateLikeRequest
{
    /// <summary>
    /// ID del post a dar like
    /// </summary>
    public int PostId { get; set; }

    /// <summary>
    /// Tipo de reacción (Like, Love, Sad, Angry, Wow, Haha, Care)
    /// </summary>
    public ReactionType ReactionType { get; set; } = ReactionType.Like;
}
