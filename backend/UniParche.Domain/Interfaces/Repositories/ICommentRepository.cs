using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Repositories;

/// <summary>
/// Repositorio específico para manejar operaciones de comentarios en la red social
/// </summary>
public interface ICommentRepository : IGenericRepository<Comment>
{
    /// <summary>
    /// Obtiene todos los comentarios de un post específico
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>Colección de comentarios del post</returns>
    Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);

    /// <summary>
    /// Obtiene todos los comentarios realizados por un usuario
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <returns>Colección de comentarios del usuario</returns>
    Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(int userId);

    /// <summary>
    /// Obtiene los comentarios más recientes de un post con paginación
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <param name="skip">Número de registros a saltar</param>
    /// <param name="take">Número de registros a obtener</param>
    /// <returns>Comentarios paginados ordenados por fecha más reciente</returns>
    Task<IEnumerable<Comment>> GetCommentsByPostWithPaginationAsync(int postId, int skip, int take);

    /// <summary>
    /// Cuenta el total de comentarios en un post
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>Número total de comentarios</returns>
    Task<int> CountCommentsByPostAsync(int postId);

    /// <summary>
    /// Obtiene comentarios con información del usuario (Include)
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>Comentarios con sus usuarios asociados</returns>
    Task<IEnumerable<Comment>> GetCommentsByPostWithUserAsync(int postId);

    /// <summary>
    /// Verifica si un usuario ha comentado en un post específico
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <param name="postId">Identificador del post</param>
    /// <returns>True si existe el comentario, False en caso contrario</returns>
    Task<bool> HasCommentedAsync(int userId, int postId);

    /// <summary>
    /// Obtiene comentarios recientes de una universidad (a través de los posts de sus usuarios)
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="take">Número de comentarios a obtener</param>
    /// <returns>Comentarios recientes de la universidad</returns>
    Task<IEnumerable<Comment>> GetRecentCommentsByUniversityAsync(int universityId, int take);
}
