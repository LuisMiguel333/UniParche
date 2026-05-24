using System;
using System.Collections.Generic;
using System.Text;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Repositories;

namespace UniParche.Domain.Helpers;

/// <summary>
/// Helper para validaciones comunes de posts, comentarios y likes
/// </summary>
public class PostValidationHelper
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly ILikeRepository _likeRepository;

    public PostValidationHelper(
        IPostRepository postRepository,
        IUserRepository userRepository,
        ICommentRepository commentRepository,
        ILikeRepository likeRepository)
    {
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        _likeRepository = likeRepository ?? throw new ArgumentNullException(nameof(likeRepository));
    }

    /// <summary>
    /// Valida que un post exista
    /// </summary>
    public async Task<Post> ValidatePostExistsAsync(int postId)
    {
        var post = await _postRepository.GetByIdAsync(postId);
        if (post == null)
            throw new KeyNotFoundException($"No se encontró el post con ID {postId}");
        return post;
    }

    /// <summary>
    /// Valida que un comentario exista
    /// </summary>
    public async Task<Comment> ValidateCommentExistsAsync(int commentId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);
        if (comment == null)
            throw new KeyNotFoundException($"No se encontró el comentario con ID {commentId}");
        return comment;
    }

    /// <summary>
    /// Valida que un like exista
    /// </summary>
    public async Task<Like> ValidateLikeExistsAsync(int likeId)
    {
        var like = await _likeRepository.GetByIdAsync(likeId);
        if (like == null)
            throw new KeyNotFoundException($"No se encontró el like con ID {likeId}");
        return like;
    }

    /// <summary>
    /// Valida que un usuario exista
    /// </summary>
    public async Task<User> ValidateUserExistsAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new KeyNotFoundException($"No se encontró el usuario con ID {userId}");
        return user;
    }

    /// <summary>
    /// Valida que el post pertenezca al usuario (propiedad)
    /// </summary>
    public async Task<Post> ValidatePostOwnershipAsync(int postId, int userId)
    {
        var post = await ValidatePostExistsAsync(postId);
        if (post.UserId != userId)
            throw new InvalidOperationException("No tienes permisos para modificar este post");
        return post;
    }

    /// <summary>
    /// Valida que el comentario pertenezca al usuario (propiedad)
    /// </summary>
    public async Task<Comment> ValidateCommentOwnershipAsync(int commentId, int userId)
    {
        var comment = await ValidateCommentExistsAsync(commentId);
        if (comment.UserId != userId)
            throw new InvalidOperationException("No tienes permisos para modificar este comentario");
        return comment;
    }

    /// <summary>
    /// Valida que el like pertenezca al usuario
    /// </summary>
    public async Task<Like> ValidateLikeOwnershipAsync(int likeId, int userId)
    {
        var like = await ValidateLikeExistsAsync(likeId);
        if (like.UserId != userId)
            throw new InvalidOperationException("No tienes permisos para modificar este like");
        return like;
    }

    /// <summary>
    /// Valida el contenido de un post
    /// </summary>
    public static void ValidatePostContent(string title, string content)
    {
        if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("El post debe tener título o contenido");
    }

    /// <summary>
    /// Valida el contenido de un comentario
    /// </summary>
    public static void ValidateCommentContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("El contenido del comentario es requerido");
        if (content.Length > 1000)
            throw new ArgumentException("El comentario no puede superar los 1000 caracteres");
    }

    /// <summary>
    /// Valida que el usuario no haya intentado likear su propio post
    /// </summary>
    public async Task ValidateLikeConstraintsAsync(int userId, int postId)
    {
        var post = await ValidatePostExistsAsync(postId);
        if (post.UserId == userId)
            throw new InvalidOperationException("No puedes dar like a tu propio post");
    }

    /// <summary>
    /// Valida que el usuario no haya intentado comentar su propio post más de una vez muy rápido
    /// </summary>
    public static void ValidateRateLimit(DateTime lastAction, int secondsLimit = 5)
    {
        var timeSinceLastAction = DateTime.UtcNow - lastAction;
        if (timeSinceLastAction.TotalSeconds < secondsLimit)
            throw new InvalidOperationException($"Debes esperar {secondsLimit} segundos antes de realizar esta acción nuevamente");
    }

    /// <summary>
    /// Valida valores de paginación
    /// </summary>
    public static void ValidatePagination(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0)
            throw new ArgumentException("El número de página debe ser mayor a 0", nameof(pageNumber));
        if (pageSize <= 0)
            throw new ArgumentException("El tamaño de página debe ser mayor a 0", nameof(pageSize));
        if (pageSize > 100)
            throw new ArgumentException("El tamaño de página no puede ser mayor a 100", nameof(pageSize));
    }

    /// <summary>
    /// Valida parámetros de búsqueda
    /// </summary>
    public static void ValidateSearchTerm(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            throw new ArgumentException("El término de búsqueda no puede estar vacío");
        if (searchTerm.Length < 2)
            throw new ArgumentException("El término de búsqueda debe tener al menos 2 caracteres");
    }
}
