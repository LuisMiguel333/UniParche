using System;
using System.Collections.Generic;
using System.Text;
using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Services;

/// <summary>
/// Servicio específico para manejar operaciones de negocio de posts
/// </summary>
public interface IPostService
{
    /// <summary>
    /// Obtiene todos los posts de forma asincrónica
    /// </summary>
    Task<IEnumerable<Post>> GetAllPostsAsync();

    /// <summary>
    /// Obtiene un post por su identificador
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>El post si existe, null en caso contrario</returns>
    Task<Post?> GetPostByIdAsync(int postId);

    /// <summary>
    /// Obtiene un post con toda su información relacionada (usuario, comentarios, likes)
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>Post con detalles completos</returns>
    Task<Post?> GetPostWithDetailsAsync(int postId);

    /// <summary>
    /// Obtiene todos los posts de un usuario específico
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <returns>Colección de posts del usuario</returns>
    Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId);

    /// <summary>
    /// Obtiene posts de un usuario con paginación
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <param name="pageNumber">Número de página</param>
    /// <param name="pageSize">Cantidad de posts por página</param>
    /// <returns>Posts paginados</returns>
    Task<(IEnumerable<Post> posts, int totalCount)> GetPostsByUserPaginatedAsync(int userId, int pageNumber, int pageSize);

    /// <summary>
    /// Obtiene todos los posts de una universidad (a través de sus usuarios)
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <returns>Colección de posts de la universidad</returns>
    Task<IEnumerable<Post>> GetPostsByUniversityIdAsync(int universityId);

    /// <summary>
    /// Obtiene los posts más recientes con paginación
    /// </summary>
    /// <param name="pageNumber">Número de página</param>
    /// <param name="pageSize">Cantidad de posts por página</param>
    /// <returns>Posts paginados ordenados por fecha reciente</returns>
    Task<(IEnumerable<Post> posts, int totalCount)> GetRecentPostsAsync(int pageNumber, int pageSize);

    /// <summary>
    /// Obtiene los posts más recientes de una universidad con paginación
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="pageNumber">Número de página</param>
    /// <param name="pageSize">Cantidad de posts por página</param>
    /// <returns>Posts paginados de la universidad</returns>
    Task<(IEnumerable<Post> posts, int totalCount)> GetRecentUniversityPostsAsync(int universityId, int pageNumber, int pageSize);

    /// <summary>
    /// Busca posts por palabras clave en el título o contenido
    /// </summary>
    /// <param name="searchTerm">Palabra clave a buscar</param>
    /// <returns>Posts que coinciden con la búsqueda</returns>
    Task<IEnumerable<Post>> SearchPostsAsync(string searchTerm);

    /// <summary>
    /// Obtiene los posts más populares (con más likes)
    /// </summary>
    /// <param name="take">Cantidad de posts a obtener</param>
    /// <returns>Posts ordenados por popularidad</returns>
    Task<IEnumerable<Post>> GetMostPopularPostsAsync(int take);

    /// <summary>
    /// Crea un nuevo post
    /// </summary>
    /// <param name="post">Datos del post a crear</param>
    /// <returns>Post creado con su ID asignado</returns>
    Task<Post> CreatePostAsync(Post post);

    /// <summary>
    /// Actualiza un post existente
    /// </summary>
    /// <param name="post">Datos del post a actualizar</param>
    /// <returns>Post actualizado</returns>
    Task<Post> UpdatePostAsync(Post post);

    /// <summary>
    /// Elimina un post
    /// </summary>
    /// <param name="postId">Identificador del post a eliminar</param>
    /// <returns>True si se eliminó correctamente</returns>
    Task<bool> DeletePostAsync(int postId);

    /// <summary>
    /// Obtiene los posts en tendencia (más interacción en tiempo reciente)
    /// </summary>
    /// <param name="take">Cantidad de posts a obtener</param>
    /// <returns>Posts en tendencia</returns>
    Task<IEnumerable<Post>> GetTrendingPostsAsync(int take);

    /// <summary>
    /// Obtiene los posts en tendencia de una universidad específica
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="take">Cantidad de posts a obtener</param>
    /// <returns>Posts en tendencia de la universidad</returns>
    Task<IEnumerable<Post>> GetTrendingUniversityPostsAsync(int universityId, int take);

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
    /// Obtiene el feed personalizado de un usuario (posts de su universidad)
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <param name="pageNumber">Número de página</param>
    /// <param name="pageSize">Cantidad de posts por página</param>
    /// <returns>Feed personalizado del usuario</returns>
    Task<(IEnumerable<Post> posts, int totalCount)> GetUserFeedAsync(int userId, int pageNumber, int pageSize);

    /// <summary>
    /// Incrementa el contador de visualizaciones de un post
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>True si se incrementó correctamente</returns>
    Task<bool> IncrementViewCountAsync(int postId);

    /// <summary>
    /// Obtiene estadísticas de un post (likes, comentarios, visualizaciones)
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>Estadísticas del post</returns>
    Task<PostStatistics?> GetPostStatisticsAsync(int postId);
}

/// <summary>
/// Clase para almacenar estadísticas de un post
/// </summary>
public class PostStatistics
{
    public int PostId { get; set; }
    public int TotalLikes { get; set; }
    public int TotalComments { get; set; }
    public int TotalViews { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
