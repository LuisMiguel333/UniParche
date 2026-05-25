namespace UniParche.API.DTOs.Response;

/// <summary>
/// DTO de respuesta para los miembros de un grupo universitario
/// </summary>
public class GroupMemberResponse
{
    public int Id { get; set; }
    public int GroupId { get; set; }
    public string GroupName { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Rol del miembro: Member, Moderator, Admin
    /// </summary>
    public string Role { get; set; } = string.Empty;
}