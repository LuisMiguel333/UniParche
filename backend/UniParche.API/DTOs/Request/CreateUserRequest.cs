namespace UniParche.API.DTOs.Request;

/// <summary>
/// DTO para crear un nuevo usuario
/// </summary>
public class CreateUserRequest
{
    /// <summary>
    /// Nombre de usuario único
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Email del usuario
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Contraseña del usuario
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Carrera/programa del usuario
    /// </summary>
    public string CareerName { get; set; } = string.Empty;

    /// <summary>
    /// Semestre actual del usuario
    /// </summary>
    public int Semester { get; set; }

    /// <summary>
    /// ID de la universidad
    /// </summary>
    public int UniversityId { get; set; }
}
