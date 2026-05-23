using System;
using System.Collections.Generic;
using System.Text;
using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Services;

/// <summary>
/// Servicio específico para manejar operaciones de negocio de likes
/// </summary>
public interface ILikeService
{
    /// <summary>
    /// Obtiene todos los likes de forma asincrónica
    /// </summary>
    Task<IEnumerable<Like>> GetAllLikesAsync();

    /// <summary>
    /// Obtiene un like por su identificador
    /// </summary>
    /// <param name="likeId">Identificador del like</param>
    /// <returns>El like si existe, null en caso contrario</returns>
    Task<Like?> GetLikeByIdAsync(int likeId);

    /// <summary>
    /// Obtiene todos los likes de un post específico
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>Colección de likes del post</returns>
    Task<IEnumerable<Like>> GetLikesByPostIdAsync(int postId);

    /// <summary>
    /// Obtiene likes de un post con paginación
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <param name="pageNumber">Número de página</param>
    /// <param name="pageSize">Cantidad de likes por página</param>
    /// <returns>Likes paginados ordenados por fecha reciente</returns>
    Task<(IEnumerable<Like> likes, int totalCount)> GetLikesByPostPaginatedAsync(int postId, int pageNumber, int pageSize);

    /// <summary>
    /// Obtiene todos los likes realizados por un usuario
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <returns>Colección de likes del usuario</returns>
    Task<IEnumerable<Like>> GetLikesByUserIdAsync(int userId);

    /// <summary>
    /// Obtiene los usuarios que han dado like a un post (con información del usuario)
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>Colección de likes con información del usuario</returns>
    Task<IEnumerable<Like>> GetLikesWithUserAsync(int postId);

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
    /// Agrega un like (usuario da like a un post)
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <param name="postId">Identificador del post</param>
    /// <returns>El like creado</returns>
    Task<Like> AddLikeAsync(int userId, int postId);

    /// <summary>
    /// Elimina un like (usuario quita like a un post)
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <param name="postId">Identificador del post</param>
    /// <returns>True si se eliminó correctamente, False si no existía el like</returns>
    Task<bool> RemoveLikeAsync(int userId, int postId);

    /// <summary>
    /// Elimina un like por su identificador
    /// </summary>
    /// <param name="likeId">Identificador del like</param>
    /// <returns>True si se eliminó correctamente</returns>
    Task<bool> DeleteLikeAsync(int likeId);

    /// <summary>
    /// Cuenta el total de likes en un post
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>Número total de likes</returns>
    Task<int> CountLikesByPostAsync(int postId);

    /// <summary>
    /// Cuenta el total de likes realizados por un usuario
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <returns>Número total de likes</returns>
    Task<int> CountLikesByUserAsync(int userId);

    /// <summary>
    /// Obtiene los posts más populares (con más likes) de toda la red
    /// </summary>
    /// <param name="take">Cantidad de posts a obtener</param>
    /// <returns>Posts ordenados por cantidad de likes</returns>
    Task<IEnumerable<Post>> GetMostLikedPostsAsync(int take);

    /// <summary>
    /// Obtiene los posts más populares de una universidad específica
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="take">Cantidad de posts a obtener</param>
    /// <returns>Posts de la universidad ordenados por likes</returns>
    Task<IEnumerable<Post>> GetMostLikedPostsByUniversityAsync(int universityId, int take);

    /// <summary>
    /// Obtiene los usuarios que más han dado likes (más activos)
    /// </summary>
    /// <param name="take">Cantidad de usuarios a obtener</param>
    /// <returns>Usuarios ordenados por cantidad de likes dados</returns>
    Task<IEnumerable<User>> GetMostActiveUsersAsync(int take);

    /// <summary>
    /// Obtiene los usuarios de una universidad que más han dado likes
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="take">Cantidad de usuarios a obtener</param>
    /// <returns>Usuarios de la universidad ordenados por likes</returns>
    Task<IEnumerable<User>> GetMostActiveUsersByUniversityAsync(int universityId, int take);

    /// <summary>
    /// Elimina todos los likes de un post (usado cuando se elimina un post)
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>Número de likes eliminados</returns>
    Task<int> DeleteLikesByPostAsync(int postId);

    /// <summary>
    /// Obtiene los likes más recientes de toda la red social
    /// </summary>
    /// <param name="take">Cantidad de likes a obtener</param>
    /// <returns>Likes recientes</returns>
    Task<IEnumerable<Like>> GetRecentLikesAsync(int take);

    /// <summary>
    /// Obtiene los likes recientes de una universidad
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="take">Cantidad de likes a obtener</param>
    /// <returns>Likes recientes de la universidad</returns>
    Task<IEnumerable<Like>> GetRecentLikesByUniversityAsync(int universityId, int take);

    /// <summary>
    /// Alterna el like de un usuario (agrega si no existe, elimina si existe)
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <param name="postId">Identificador del post</param>
    /// <returns>True si se agregó like, False si se eliminó</returns>
    Task<bool> ToggleLikeAsync(int userId, int postId);

    /// <summary>
    /// Obtiene estadísticas de likes en un período de tiempo
    /// </summary>
    /// <param name="universityId">Identificador de la universidad (opcional)</param>
    /// <param name="days">Número de días a considerar</param>
    /// <returns>Estadísticas de likes</returns>
    Task<LikeStatistics?> GetLikeStatisticsAsync(int? universityId, int days);
}

/// <summary>
/// Clase para almacenar estadísticas de likes
/// </summary>
public class LikeStatistics
{
    public int TotalLikes { get; set; }
    public int TotalPosts { get; set; }
    public double AverageLikesPerPost { get; set; }
    public int MostLikedPostId { get; set; }
    public int MostLikesCount { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
}
