using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using UniParche.Domain.Entities;
using UniParche.Domain.Helpers;
using UniParche.Domain.Interfaces.Repositories;
using UniParche.Domain.Interfaces.Services;

namespace UniParche.Domain.Services;

/// <summary>
/// Implementación del servicio de posts con lógica de negocio
/// </summary>
public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly ILikeRepository _likeRepository;
    private readonly PostValidationHelper _validationHelper;
    private readonly ILogger<PostService> _logger;

    public PostService(
        IPostRepository postRepository,
        ICommentRepository commentRepository,
        ILikeRepository likeRepository,
        PostValidationHelper validationHelper,
        ILogger<PostService> logger)
    {
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        _likeRepository = likeRepository ?? throw new ArgumentNullException(nameof(likeRepository));
        _validationHelper = validationHelper ?? throw new ArgumentNullException(nameof(validationHelper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    // ═══ Obtención de Posts ═══

    public async Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        _logger.LogInformation("Obteniendo todos los posts");
        return await _postRepository.GetAllAsync();
    }

    public async Task<Post?> GetPostByIdAsync(int postId)
    {
        if (postId <= 0)
            return null;

        _logger.LogInformation("Obteniendo post con ID {PostId}", postId);
        return await _postRepository.GetByIdAsync(postId);
    }

    public async Task<Post?> GetPostWithDetailsAsync(int postId)
    {
        var post = await _validationHelper.ValidatePostExistsAsync(postId);
        _logger.LogInformation("Obteniendo post con detalles: {PostId}", postId);
        return await _postRepository.GetPostWithDetailsAsync(postId);
    }

    public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId)
    {
        if (userId <= 0)
            return Enumerable.Empty<Post>();

        _logger.LogInformation("Obteniendo posts del usuario {UserId}", userId);
        return await _postRepository.GetPostsByUserIdAsync(userId);
    }

    public async Task<(IEnumerable<Post> posts, int totalCount)> GetPostsByUserPaginatedAsync(int userId, int pageNumber, int pageSize)
    {
        PostValidationHelper.ValidatePagination(pageNumber, pageSize);
        await _validationHelper.ValidateUserExistsAsync(userId);

        _logger.LogInformation("Obteniendo posts del usuario {UserId} - Página {Page}", userId, pageNumber);

        var posts = await _postRepository.GetPostsByUserIdAsync(userId);
        var totalCount = posts.Count();

        var paginatedPosts = posts
            .OrderByDescending(p => p.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return (paginatedPosts, totalCount);
    }

    public async Task<IEnumerable<Post>> GetPostsByUniversityIdAsync(int universityId)
    {
        if (universityId <= 0)
            return Enumerable.Empty<Post>();

        _logger.LogInformation("Obteniendo posts de la universidad {UniversityId}", universityId);
        return await _postRepository.GetPostsByUniversityIdAsync(universityId);
    }

    public async Task<(IEnumerable<Post> posts, int totalCount)> GetRecentPostsAsync(int pageNumber, int pageSize)
    {
        PostValidationHelper.ValidatePagination(pageNumber, pageSize);
        _logger.LogInformation("Obteniendo posts recientes - Página {Page}", pageNumber);

        var posts = await _postRepository.GetAllAsync();
        var totalCount = posts.Count();

        var paginatedPosts = posts
            .OrderByDescending(p => p.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return (paginatedPosts, totalCount);
    }

    public async Task<(IEnumerable<Post> posts, int totalCount)> GetRecentUniversityPostsAsync(int universityId, int pageNumber, int pageSize)
    {
        PostValidationHelper.ValidatePagination(pageNumber, pageSize);

        _logger.LogInformation("Obteniendo posts recientes de universidad {UniversityId} - Página {Page}", universityId, pageNumber);

        var posts = await _postRepository.GetRecentUniversityPostsAsync(universityId, (pageNumber - 1) * pageSize, pageSize);
        var totalCount = await _postRepository.CountPostsByUniversityAsync(universityId);

        return (posts, totalCount);
    }

    public async Task<IEnumerable<Post>> SearchPostsAsync(string searchTerm)
    {
        PostValidationHelper.ValidateSearchTerm(searchTerm);
        _logger.LogInformation("Buscando posts: {SearchTerm}", searchTerm);
        return await _postRepository.SearchPostsAsync(searchTerm);
    }

    public async Task<IEnumerable<Post>> GetMostPopularPostsAsync(int take)
    {
        if (take <= 0)
            return Enumerable.Empty<Post>();

        _logger.LogInformation("Obteniendo {Count} posts más populares", take);
        return await _postRepository.GetMostPopularPostsAsync(take);
    }

    // ═══ Crear, Actualizar y Eliminar Posts ═══

    public async Task<Post> CreatePostAsync(Post post)
    {
        if (post == null)
            throw new ArgumentNullException(nameof(post));

        // Validaciones
        if (post.UserId <= 0)
            throw new ArgumentException("El ID del usuario es requerido", nameof(post.UserId));

        PostValidationHelper.ValidatePostContent(post.Title, post.Content);
        await _validationHelper.ValidateUserExistsAsync(post.UserId);

        post.CreatedAt = DateTime.UtcNow;
        post.UpdatedAt = DateTime.UtcNow;

        _logger.LogInformation("Creando nuevo post del usuario {UserId}", post.UserId);
        return await _postRepository.AddAsync(post);
    }

    public async Task<Post> UpdatePostAsync(Post post)
    {
        if (post == null)
            throw new ArgumentNullException(nameof(post));

        if (post.Id <= 0)
            throw new ArgumentException("El post debe tener un ID válido", nameof(post.Id));

        await _validationHelper.ValidatePostExistsAsync(post.Id);
        PostValidationHelper.ValidatePostContent(post.Title, post.Content);

        post.UpdatedAt = DateTime.UtcNow;

        _logger.LogInformation("Actualizando post: {PostId}", post.Id);
        return await _postRepository.UpdateAsync(post);
    }

    public async Task<bool> DeletePostAsync(int postId)
    {
        var post = await _validationHelper.ValidatePostExistsAsync(postId);

        // Eliminar comentarios del post
        var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);
        foreach (var comment in comments)
        {
            await _commentRepository.DeleteAsync(comment);
        }

        // Eliminar likes del post
        await _likeRepository.DeleteLikesByPostAsync(postId);

        _logger.LogInformation("Eliminando post: {PostId}", postId);
        return await _postRepository.DeleteAsync(postId);
    }

    // ═══ Posts en Tendencia ═══

    public async Task<IEnumerable<Post>> GetTrendingPostsAsync(int take)
    {
        if (take <= 0)
            return Enumerable.Empty<Post>();

        _logger.LogInformation("Obteniendo posts en tendencia");
        return await _postRepository.GetMostPopularPostsAsync(take);
    }

    public async Task<IEnumerable<Post>> GetTrendingUniversityPostsAsync(int universityId, int take)
    {
        if (universityId <= 0 || take <= 0)
            return Enumerable.Empty<Post>();

        _logger.LogInformation("Obteniendo posts en tendencia de universidad {UniversityId}", universityId);

        var universityPosts = await _postRepository.GetPostsByUniversityIdAsync(universityId);

        return universityPosts
            .OrderByDescending(p => p.CreatedAt)
            .Take(take);
    }

    // ═══ Feed y Consultas Especializadas ═══

    public async Task<(IEnumerable<Post> posts, int totalCount)> GetUserFeedAsync(int userId, int pageNumber, int pageSize)
    {
        PostValidationHelper.ValidatePagination(pageNumber, pageSize);
        var user = await _validationHelper.ValidateUserExistsAsync(userId);

        _logger.LogInformation("Obteniendo feed del usuario {UserId} - Página {Page}", userId, pageNumber);

        // Obtener posts de la universidad del usuario
        var universityPosts = await _postRepository.GetPostsByUniversityIdAsync(user.UniversityId);
        var totalCount = universityPosts.Count();

        var paginatedPosts = universityPosts
            .OrderByDescending(p => p.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return (paginatedPosts, totalCount);
    }

    public async Task<bool> IncrementViewCountAsync(int postId)
    {
        var post = await _validationHelper.ValidatePostExistsAsync(postId);
        _logger.LogInformation("Incrementando vistas del post: {PostId}", postId);
        return true;
    }

    // ═══ Estadísticas ═══

    public async Task<PostStatistics?> GetPostStatisticsAsync(int postId)
    {
        var post = await _validationHelper.ValidatePostExistsAsync(postId);

        _logger.LogInformation("Obteniendo estadísticas del post: {PostId}", postId);

        var totalLikes = await _likeRepository.CountLikesByPostAsync(postId);
        var totalComments = await _commentRepository.CountCommentsByPostAsync(postId);

        return new PostStatistics
        {
            PostId = postId,
            TotalLikes = totalLikes,
            TotalComments = totalComments,
            TotalViews = 0,
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt
        };
    }

    public async Task<int> CountPostsByUserAsync(int userId)
    {
        if (userId <= 0)
            return 0;

        return await _postRepository.CountPostsByUserAsync(userId);
    }

    public async Task<int> CountPostsByUniversityAsync(int universityId)
    {
        if (universityId <= 0)
            return 0;

        return await _postRepository.CountPostsByUniversityAsync(universityId);
    }
}
