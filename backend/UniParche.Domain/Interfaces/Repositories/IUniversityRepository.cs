using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Repositories;

/// <summary>
/// Repositorio específico para manejar operaciones de universidades en la red social
/// </summary>
public interface IUniversityRepository : IGenericRepository<University>
{
    /// <summary>
    /// Obtiene una universidad por su dominio de email
    /// </summary>
    /// <param name="domainEmail">Dominio de email de la universidad</param>
    /// <returns>La universidad si existe, null en caso contrario</returns>
    Task<University?> GetByDomainEmailAsync(string domainEmail);

    /// <summary>
    /// Obtiene una universidad con todos sus usuarios
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <returns>Universidad con información de usuarios</returns>
    Task<University?> GetUniversityWithUsersAsync(int universityId);

    /// <summary>
    /// Obtiene una universidad con todos sus posts (a través de sus usuarios)
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <returns>Universidad con información de posts</returns>
    Task<University?> GetUniversityWithPostsAsync(int universityId);

    /// <summary>
    /// Obtiene todas las universidades con información de cantidad de usuarios
    /// </summary>
    /// <returns>Colección de universidades con conteos</returns>
    Task<IEnumerable<University>> GetUniversitiesWithUserCountAsync();

    /// <summary>
    /// Busca universidades por nombre
    /// </summary>
    /// <param name="searchTerm">Palabra clave a buscar</param>
    /// <returns>Universidades que coinciden con la búsqueda</returns>
    Task<IEnumerable<University>> SearchUniversitiesAsync(string searchTerm);

    /// <summary>
    /// Cuenta el total de usuarios en una universidad
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <returns>Número total de usuarios</returns>
    Task<int> CountUsersByUniversityAsync(int universityId);

    /// <summary>
    /// Cuenta el total de posts en una universidad
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <returns>Número total de posts</returns>
    Task<int> CountPostsByUniversityAsync(int universityId);

    /// <summary>
    /// Obtiene las universidades más activas (con más posts o actividad)
    /// </summary>
    /// <param name="take">Número de universidades a obtener</param>
    /// <returns>Universidades ordenadas por actividad</returns>
    Task<IEnumerable<University>> GetMostActiveUniversitiesAsync(int take);

    /// <summary>
    /// Obtiene las universidades con más usuarios
    /// </summary>
    /// <param name="take">Número de universidades a obtener</param>
    /// <returns>Universidades ordenadas por cantidad de usuarios</returns>
    Task<IEnumerable<University>> GetUniversitiesWithMostUsersAsync(int take);

    /// <summary>
    /// Verifica si existe una universidad con un nombre específico
    /// </summary>
    /// <param name="name">Nombre de la universidad</param>
    /// <returns>True si existe, False en caso contrario</returns>
    Task<bool> UniversityExistsByNameAsync(string name);

    /// <summary>
    /// Verifica si existe una universidad con un dominio de email específico
    /// </summary>
    /// <param name="domainEmail">Dominio de email</param>
    /// <returns>True si existe, False en caso contrario</returns>
    Task<bool> UniversityExistsByDomainAsync(string domainEmail);

    /// <summary>
    /// Obtiene estadísticas de una universidad (usuarios, posts, likes)
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <returns>Objeto con estadísticas de la universidad</returns>
    Task<UniversityStatistics?> GetUniversityStatisticsAsync(int universityId);
}

/// <summary>
/// Clase para almacenar estadísticas de una universidad
/// </summary>
public class UniversityStatistics
{
    public int UniversityId { get; set; }
    public string UniversityName { get; set; } = string.Empty;
    public int TotalUsers { get; set; }
    public int TotalPosts { get; set; }
    public int TotalComments { get; set; }
    public int TotalLikes { get; set; }
}
