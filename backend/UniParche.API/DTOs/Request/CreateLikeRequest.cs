namespace UniParche.API.DTOs.Request;

/// <summary>
/// DTO para crear/dar un like
/// </summary>
public class CreateLikeRequest
{
    /// <summary>
    /// ID del post a dar like
    /// </summary>
    public int PostId { get; set; }
}
