namespace UniParche.API.DTOs.Response;

/// <summary>
/// DTO de respuesta para información del usuario
/// </summary>
public class UserResponse
{
    /// <summary>
    /// ID del usuario
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre de usuario
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Email del usuario
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Carrera/programa del usuario
    /// </summary>
    public string CareerName { get; set; } = string.Empty;

    /// <summary>
    /// Semestre actual del usuario
    /// </summary>
    public int Semester { get; set; }

    /// <summary>
    /// URL de la foto de perfil
    /// </summary>
    public string ProfilePictureUrl { get; set; } = string.Empty;

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime RegisterDate { get; set; }

    /// <summary>
    /// ID de la universidad
    /// </summary>
    public int UniversityId { get; set; }

    /// <summary>
    /// Nombre de la universidad
    /// </summary>
    public string UniversityName { get; set; } = string.Empty;
}
