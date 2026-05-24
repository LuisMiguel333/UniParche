namespace UniParche.API.DTOs.Request;

/// <summary>
/// DTO para actualizar la contraseña del usuario
/// </summary>
public class UpdatePasswordRequest
{
    /// <summary>
    /// Contraseña actual
    /// </summary>
    public string CurrentPassword { get; set; } = string.Empty;

    /// <summary>
    /// Nueva contraseña
    /// </summary>
    public string NewPassword { get; set; } = string.Empty;

    /// <summary>
    /// Confirmación de la nueva contraseña
    /// </summary>
    public string ConfirmPassword { get; set; } = string.Empty;
}
