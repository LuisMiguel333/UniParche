namespace UniParche.API.DTOs.Request;

/// <summary>
/// DTO base para solicitudes con paginación
/// </summary>
public class PaginationRequest
{
    /// <summary>
    /// Número de página (comenzando en 1)
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Cantidad de registros por página
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Validar y ajustar parámetros de paginación
    /// </summary>
    public void Validate()
    {
        if (PageNumber < 1) PageNumber = 1;
        if (PageSize < 1) PageSize = 10;
        if (PageSize > 100) PageSize = 100; // Máximo 100 registros por página
    }
}

/// <summary>
/// DTO para búsqueda con paginación
/// </summary>
public class SearchRequest : PaginationRequest
{
    /// <summary>
    /// Término de búsqueda
    /// </summary>
    public string? SearchTerm { get; set; }

    /// <summary>
    /// Campo por el cual ordenar
    /// </summary>
    public string? SortBy { get; set; } = "CreatedAt";

    /// <summary>
    /// Dirección de ordenamiento (asc/desc)
    /// </summary>
    public string? SortDirection { get; set; } = "desc";
}
