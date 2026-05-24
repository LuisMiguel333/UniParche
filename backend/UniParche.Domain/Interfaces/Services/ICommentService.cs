using System;
using System.Collections.Generic;
using System.Text;
using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Services;

/// <summary>
/// Servicio específico para manejar operaciones de negocio de comentarios
/// </summary>
public interface ICommentService
{
    /// <summary>
    /// Obtiene todos los comentarios de forma asincrónica
    /// </summary>
    Task<IEnumerable<Comment>> GetAllCommentsAsync();

    /// <summary>
    /// Obtiene un comentario por su identificador
    /// </summary>
    /// <param name="commentId">Identificador del comentario</param>
    /// <returns>El comentario si existe, null en caso contrario</returns>
    Task<Comment?> GetCommentByIdAsync(int commentId);

    /// <summary>
    /// Obtiene todos los comentarios de un post específico
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>Colección de comentarios del post</returns>
    Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);

    /// <summary>
    /// Obtiene comentarios de un post con paginación
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <param name="pageNumber">Número de página</param>
    /// <param name="pageSize">Cantidad de comentarios por página</param>
    /// <returns>Comentarios paginados ordenados por fecha reciente</returns>
    Task<(IEnumerable<Comment> comments, int totalCount)> GetCommentsByPostPaginatedAsync(int postId, int pageNumber, int pageSize);

    /// <summary>
    /// Obtiene todos los comentarios realizados por un usuario
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <returns>Colección de comentarios del usuario</returns>
    Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(int userId);

    /// <summary>
    /// Obtiene comentarios con información del usuario (Include)
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>Comentarios con información del usuario</returns>
    Task<IEnumerable<Comment>> GetCommentsByPostWithUserAsync(int postId);

    /// <summary>
    /// Crea un nuevo comentario
    /// </summary>
    /// <param name="comment">Datos del comentario a crear</param>
    /// <returns>Comentario creado con su ID asignado</returns>
    Task<Comment> CreateCommentAsync(Comment comment);

    /// <summary>
    /// Actualiza un comentario existente
    /// </summary>
    /// <param name="comment">Datos del comentario a actualizar</param>
    /// <returns>Comentario actualizado</returns>
    Task<Comment> UpdateCommentAsync(Comment comment);

    /// <summary>
    /// Elimina un comentario
    /// </summary>
    /// <param name="commentId">Identificador del comentario a eliminar</param>
    /// <returns>True si se eliminó correctamente</returns>
    Task<bool> DeleteCommentAsync(int commentId);

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
    /// <param name="take">Cantidad de comentarios a obtener</param>
    /// <returns>Comentarios recientes de la universidad</returns>
    Task<IEnumerable<Comment>> GetRecentCommentsByUniversityAsync(int universityId, int take);

    /// <summary>
    /// Cuenta el total de comentarios en un post
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>Número total de comentarios</returns>
    Task<int> CountCommentsByPostAsync(int postId);

    /// <summary>
    /// Cuenta el total de comentarios realizados por un usuario
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <returns>Número total de comentarios</returns>
    Task<int> CountCommentsByUserAsync(int userId);

    /// <summary>
    /// Obtiene los usuarios más activos comentando en un post
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <param name="take">Cantidad de usuarios a obtener</param>
    /// <returns>Usuarios ordenados por cantidad de comentarios</returns>
    Task<IEnumerable<User>> GetMostActiveCommentersAsync(int postId, int take);

    /// <summary>
    /// Obtiene los comentarios más recientes de la red social
    /// </summary>
    /// <param name="take">Cantidad de comentarios a obtener</param>
    /// <returns>Comentarios recientes</returns>
    Task<IEnumerable<Comment>> GetRecentCommentsAsync(int take);

    /// <summary>
    /// Busca comentarios por palabras clave en su contenido
    /// </summary>
    /// <param name="searchTerm">Palabra clave a buscar</param>
    /// <returns>Comentarios que contienen la palabra clave</returns>
    Task<IEnumerable<Comment>> SearchCommentsAsync(string searchTerm);

    /// <summary>
    /// Elimina todos los comentarios de un post (usado cuando se elimina un post)
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <returns>Número de comentarios eliminados</returns>
    Task<int> DeleteCommentsByPostAsync(int postId);

    /// <summary>
    /// Elimina todos los comentarios de un usuario
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <returns>Número de comentarios eliminados</returns>
    Task<int> DeleteCommentsByUserAsync(int userId);

    /// <summary>
    /// Obtiene comentarios ordenados por likes (si existe un sistema de votación)
    /// </summary>
    /// <param name="postId">Identificador del post</param>
    /// <param name="take">Cantidad de comentarios a obtener</param>
    /// <returns>Comentarios más votados</returns>
    Task<IEnumerable<Comment>> GetMostLikedCommentsAsync(int postId, int take);
}
