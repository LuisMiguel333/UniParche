namespace UniParche.API.DTOs.Request;

/// <summary>
/// DTO para actualizar información del usuario
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    /// Nuevo nombre de usuario
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Nueva carrera/programa
    /// </summary>
    public string? CareerName { get; set; }

    /// <summary>
    /// Nuevo semestre
    /// </summary>
    public int? Semester { get; set; }

    /// <summary>
    /// Nueva URL de foto de perfil
    /// </summary>
    public string? ProfilePictureUrl { get; set; }
}
