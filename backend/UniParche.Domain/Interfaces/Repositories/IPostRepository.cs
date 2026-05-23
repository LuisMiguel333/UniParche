using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Repositories;

/// <summary>
/// Repositorio específico para manejar operaciones de posts en la red social
/// </summary>
public interface IPostRepository : IGenericRepository<Post>
{
    /// <summary>
    /// Obtiene todos los posts de un usuario específico
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <returns>Colección de posts del usuario</returns>
    Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId);

    /// <summary>
    /// Obtiene todos los posts de una universidad (a través de sus usuarios)
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <returns>Colección de posts de la universidad</returns>
    Task<IEnumerable<Post>> GetPostsByUniversityIdAsync(int universityId);

    /// <summary>
    /// Obtiene los posts más recientes con paginación
    /// </summary>
    /// <param name="skip">Número de registros a saltar</param>
    /// <param name="take">Número de registros a obtener</param>
    /// <returns>Posts paginados ordenados por fecha más reciente</returns>
    Task<IEnumerable<Post>> GetRecentPostsAsync(int skip, int take);

    /// <summary>
    /// Obtiene los posts más recientes de una universidad con paginación
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="skip">Número de registros a saltar</param>
    /// <param name="take">Número de registros a obtener</param>
    /// <returns>Posts paginados de la universidad</returns>
    Task<IEnumerable<Post>> GetRecentUniversityPostsAsync(int universityId, int skip, int take);

    /// <summary>
    /// Obtiene un post con toda su información relacionada (usuario, comentarios, likes)
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>Post con información relacionada</returns>
    Task<Post?> GetPostWithDetailsAsync(int postId);

    /// <summary>
    /// Obtiene posts con información del usuario (Include)
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <returns>Posts con usuarios asociados</returns>
    Task<IEnumerable<Post>> GetPostsByUserWithDetailsAsync(int userId);

    /// <summary>
    /// Busca posts por palabras clave en el título o contenido
    /// </summary>
    /// <param name="searchTerm">Palabra clave a buscar</param>
    /// <returns>Posts que coinciden con la búsqueda</returns>
    Task<IEnumerable<Post>> SearchPostsAsync(string searchTerm);

    /// <summary>
    /// Obtiene los posts más populares (con más likes)
    /// </summary>
    /// <param name="take">Número de posts a obtener</param>
    /// <returns>Posts ordenados por popularidad</returns>
    Task<IEnumerable<Post>> GetMostPopularPostsAsync(int take);

    /// <summary>
    /// Cuenta el total de posts de un usuario
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <returns>Número total de posts</returns>
    Task<int> CountPostsByUserAsync(int userId);

    /// <summary>
    /// Cuenta el total de posts de una universidad
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <returns>Número total de posts</returns>
    Task<int> CountPostsByUniversityAsync(int universityId);

    /// <summary>
    /// Obtiene el feed de posts de una universidad (posts recientes de todos sus usuarios)
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="skip">Número de registros a saltar</param>
    /// <param name="take">Número de registros a obtener</param>
    /// <returns>Feed de posts de la universidad</returns>
    Task<IEnumerable<Post>> GetUniversityFeedAsync(int universityId, int skip, int take);
}
