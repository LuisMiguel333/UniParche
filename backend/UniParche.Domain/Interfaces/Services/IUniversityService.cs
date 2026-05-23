using System;
using System.Collections.Generic;
using System.Text;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Repositories;

namespace UniParche.Domain.Interfaces.Services;

/// <summary>
/// Servicio específico para manejar operaciones de negocio de universidades
/// </summary>
public interface IUniversityService
{
    /// <summary>
    /// Obtiene todas las universidades de forma asincrónica
    /// </summary>
    Task<IEnumerable<University>> GetAllUniversitiesAsync();

    /// <summary>
    /// Obtiene una universidad por su identificador
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <returns>La universidad si existe, null en caso contrario</returns>
    Task<University?> GetUniversityByIdAsync(int universityId);

    /// <summary>
    /// Obtiene una universidad por su nombre
    /// </summary>
    /// <param name="name">Nombre de la universidad</param>
    /// <returns>La universidad si existe, null en caso contrario</returns>
    Task<University?> GetUniversityByNameAsync(string name);

    /// <summary>
    /// Obtiene una universidad por su dominio de email
    /// </summary>
    /// <param name="domainEmail">Dominio de email de la universidad</param>
    /// <returns>La universidad si existe, null en caso contrario</returns>
    Task<University?> GetUniversityByDomainEmailAsync(string domainEmail);

    /// <summary>
    /// Obtiene una universidad con todos sus usuarios
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <returns>Universidad con usuarios asociados</returns>
    Task<University?> GetUniversityWithUsersAsync(int universityId);

    /// <summary>
    /// Obtiene una universidad con todos sus posts (a través de sus usuarios)
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <returns>Universidad con posts asociados</returns>
    Task<University?> GetUniversityWithPostsAsync(int universityId);

    /// <summary>
    /// Busca universidades por nombre
    /// </summary>
    /// <param name="searchTerm">Palabra clave a buscar</param>
    /// <returns>Universidades que coinciden con la búsqueda</returns>
    Task<IEnumerable<University>> SearchUniversitiesAsync(string searchTerm);

    /// <summary>
    /// Obtiene universidades con información de cantidad de usuarios
    /// </summary>
    /// <returns>Universidades con conteos de usuarios</returns>
    Task<IEnumerable<University>> GetUniversitiesWithUserCountAsync();

    /// <summary>
    /// Crea una nueva universidad
    /// </summary>
    /// <param name="university">Datos de la universidad a crear</param>
    /// <returns>Universidad creada con su ID asignado</returns>
    Task<University> CreateUniversityAsync(University university);

    /// <summary>
    /// Actualiza una universidad existente
    /// </summary>
    /// <param name="university">Datos de la universidad a actualizar</param>
    /// <returns>Universidad actualizada</returns>
    Task<University> UpdateUniversityAsync(University university);

    /// <summary>
    /// Elimina una universidad
    /// </summary>
    /// <param name="universityId">Identificador de la universidad a eliminar</param>
    /// <returns>True si se eliminó correctamente</returns>
    Task<bool> DeleteUniversityAsync(int universityId);

    /// <summary>
    /// Obtiene las universidades más activas (con más posts o actividad)
    /// </summary>
    /// <param name="take">Cantidad de universidades a obtener</param>
    /// <returns>Universidades ordenadas por actividad</returns>
    Task<IEnumerable<University>> GetMostActiveUniversitiesAsync(int take);

    /// <summary>
    /// Obtiene las universidades con más usuarios
    /// </summary>
    /// <param name="take">Cantidad de universidades a obtener</param>
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
    /// Obtiene estadísticas completas de una universidad
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <returns>Estadísticas incluidas usuarios, posts, comentarios y likes</returns>
    Task<UniversityStatistics?> GetUniversityStatisticsAsync(int universityId);

    /// <summary>
    /// Obtiene el feed principal de una universidad (posts recientes de todos sus usuarios)
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="pageNumber">Número de página</param>
    /// <param name="pageSize">Cantidad de posts por página</param>
    /// <returns>Feed de posts de la universidad</returns>
    Task<(IEnumerable<Post> posts, int totalCount)> GetUniversityFeedAsync(int universityId, int pageNumber, int pageSize);

    /// <summary>
    /// Obtiene las universidades tendencia (más activas en el tiempo)
    /// </summary>
    /// <param name="take">Cantidad de universidades a obtener</param>
    /// <returns>Universidades en tendencia</returns>
    Task<IEnumerable<University>> GetTrendingUniversitiesAsync(int take);

    /// <summary>
    /// Cuenta el total de universidades
    /// </summary>
    /// <returns>Número total de universidades</returns>
    Task<int> CountUniversitiesAsync();

    /// <summary>
    /// Obtiene universidades con paginación
    /// </summary>
    /// <param name="pageNumber">Número de página</param>
    /// <param name="pageSize">Cantidad de registros por página</param>
    /// <returns>Universidades paginadas</returns>
    Task<(IEnumerable<University> universities, int totalCount)> GetUniversitiesPaginatedAsync(int pageNumber, int pageSize);
}
