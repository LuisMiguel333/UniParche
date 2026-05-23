using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Repositories;

/// <summary>
/// Repositorio específico para manejar operaciones de likes en la red social
/// </summary>
public interface ILikeRepository : IGenericRepository<Like>
{
    /// <summary>
    /// Obtiene todos los likes de un post específico
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>Colección de likes del post</returns>
    Task<IEnumerable<Like>> GetLikesByPostIdAsync(int postId);

    /// <summary>
    /// Obtiene todos los likes realizados por un usuario
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <returns>Colección de likes del usuario</returns>
    Task<IEnumerable<Like>> GetLikesByUserIdAsync(int userId);

    /// <summary>
    /// Verifica si un usuario ha dado like a un post específico
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <param name="postId">Identificador del post</param>
    /// <returns>True si existe el like, False en caso contrario</returns>
    Task<bool> HasUserLikedPostAsync(int userId, int postId);

    /// <summary>
    /// Obtiene el like específico de un usuario a un post
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <param name="postId">Identificador del post</param>
    /// <returns>El like si existe, null en caso contrario</returns>
    Task<Like?> GetLikeAsync(int userId, int postId);

    /// <summary>
    /// Cuenta el total de likes en un post
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>Número total de likes</returns>
    Task<int> CountLikesByPostAsync(int postId);

    /// <summary>
    /// Obtiene los usuarios que han dado like a un post (con información del usuario)
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>Colección de likes con información del usuario</returns>
    Task<IEnumerable<Like>> GetLikesWithUserAsync(int postId);

    /// <summary>
    /// Obtiene los likes más recientes de un post con paginación
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <param name="skip">Número de registros a saltar</param>
    /// <param name="take">Número de registros a obtener</param>
    /// <returns>Likes paginados ordenados por fecha más reciente</returns>
    Task<IEnumerable<Like>> GetLikesByPostWithPaginationAsync(int postId, int skip, int take);

    /// <summary>
    /// Obtiene posts con más likes en un período de tiempo
    /// </summary>
    /// <param name="universityId">Identificador de la universidad (opcional)</param>
    /// <param name="take">Número de posts a obtener</param>
    /// <returns>Posts ordenados por cantidad de likes</returns>
    Task<IEnumerable<Like>> GetMostLikedPostsAsync(int? universityId, int take);

    /// <summary>
    /// Elimina todos los likes de un post (usado cuando se elimina un post)
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>Número de likes eliminados</returns>
    Task<int> DeleteLikesByPostAsync(int postId);

    /// <summary>
    /// Obtiene los usuarios de una universidad que han dado like a posts
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="take">Número de registros a obtener</param>
    /// <returns>Likes de usuarios de la universidad</returns>
    Task<IEnumerable<Like>> GetUniversityLikesAsync(int universityId, int take);
}
