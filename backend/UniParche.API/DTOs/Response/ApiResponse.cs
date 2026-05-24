namespace UniParche.API.DTOs.Response;

/// <summary>
/// DTO de respuesta para operaciones exitosas
/// </summary>
public class ApiResponse<T>
{
    /// <summary>
    /// Indica si la operación fue exitosa
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Mensaje descriptivo de la respuesta
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Datos retornados
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Errores si los hay
    /// </summary>
    public List<string>? Errors { get; set; }

    /// <summary>
    /// Constructor para respuesta exitosa
    /// </summary>
    public ApiResponse(T data, string message = "Operación completada exitosamente")
    {
        Success = true;
        Message = message;
        Data = data;
    }

    /// <summary>
    /// Constructor para respuesta de error
    /// </summary>
    public ApiResponse(string message, List<string>? errors = null)
    {
        Success = false;
        Message = message;
        Errors = errors;
    }

    /// <summary>
    /// Constructor sin parámetros (para deserialización)
    /// </summary>
    public ApiResponse() { }
}

/// <summary>
/// DTO de respuesta paginada
/// </summary>
public class PaginatedResponse<T>
{
    /// <summary>
    /// Datos del resultado actual
    /// </summary>
    public List<T> Items { get; set; } = new();

    /// <summary>
    /// Número de página actual
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Tamaño de página
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total de registros
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Total de páginas
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    /// <summary>
    /// Indica si hay página siguiente
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// Indica si hay página anterior
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;
}
