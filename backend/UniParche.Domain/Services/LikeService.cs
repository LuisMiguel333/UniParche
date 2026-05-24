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
/// Implementación del servicio de likes con lógica de negocio
/// </summary>
public class LikeService : ILikeService
{
    private readonly ILikeRepository _likeRepository;
    private readonly PostValidationHelper _validationHelper;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<LikeService> _logger;

    public LikeService(
        ILikeRepository likeRepository,
        PostValidationHelper validationHelper,
        IPostRepository postRepository,
        IUserRepository userRepository,
        ILogger<LikeService> logger)
    {
        _likeRepository = likeRepository ?? throw new ArgumentNullException(nameof(likeRepository));
        _validationHelper = validationHelper ?? throw new ArgumentNullException(nameof(validationHelper));
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    // ═══ Obtención de Likes ═══

    public async Task<IEnumerable<Like>> GetAllLikesAsync()
    {
        _logger.LogInformation("Obteniendo todos los likes");
        return await _likeRepository.GetAllAsync();
    }

    public async Task<Like?> GetLikeByIdAsync(int likeId)
    {
        if (likeId <= 0)
            return null;

        _logger.LogInformation("Obteniendo like con ID {LikeId}", likeId);
        return await _likeRepository.GetByIdAsync(likeId);
    }

    public async Task<IEnumerable<Like>> GetLikesByPostIdAsync(int postId)
    {
        if (postId <= 0)
            return Enumerable.Empty<Like>();

        await _validationHelper.ValidatePostExistsAsync(postId);
        _logger.LogInformation("Obteniendo likes del post {PostId}", postId);
        return await _likeRepository.GetLikesByPostIdAsync(postId);
    }

    public async Task<(IEnumerable<Like> likes, int totalCount)> GetLikesByPostPaginatedAsync(int postId, int pageNumber, int pageSize)
    {
        PostValidationHelper.ValidatePagination(pageNumber, pageSize);
        await _validationHelper.ValidatePostExistsAsync(postId);

        _logger.LogInformation("Obteniendo likes del post {PostId} - Página {Page}", postId, pageNumber);

        var likes = await _likeRepository.GetLikesByPostIdAsync(postId);
        var totalCount = likes.Count();

        var paginatedLikes = likes
            .OrderByDescending(l => l.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return (paginatedLikes, totalCount);
    }

    public async Task<IEnumerable<Like>> GetLikesByUserIdAsync(int userId)
    {
        if (userId <= 0)
            return Enumerable.Empty<Like>();

        await _validationHelper.ValidateUserExistsAsync(userId);
        _logger.LogInformation("Obteniendo likes del usuario {UserId}", userId);
        return await _likeRepository.GetLikesByUserIdAsync(userId);
    }

    public async Task<IEnumerable<Like>> GetLikesWithUserAsync(int postId)
    {
        if (postId <= 0)
            return Enumerable.Empty<Like>();

        await _validationHelper.ValidatePostExistsAsync(postId);
        _logger.LogInformation("Obteniendo likes con usuario del post {PostId}", postId);
        return await _likeRepository.GetLikesWithUserAsync(postId);
    }

    // ═══ Verificaciones ═══

    public async Task<bool> HasUserLikedPostAsync(int userId, int postId)
    {
        if (userId <= 0 || postId <= 0)
            return false;

        return await _likeRepository.HasUserLikedPostAsync(userId, postId);
    }

    public async Task<Like?> GetLikeAsync(int userId, int postId)
    {
        if (userId <= 0 || postId <= 0)
            return null;

        return await _likeRepository.GetLikeAsync(userId, postId);
    }

    // ═══ Agregar y Eliminar Likes ═══

    public async Task<Like> AddLikeAsync(int userId, int postId)
    {
        // Validaciones
        await _validationHelper.ValidateUserExistsAsync(userId);
        await _validationHelper.ValidatePostExistsAsync(postId);
        await _validationHelper.ValidateLikeConstraintsAsync(userId, postId);

        // Verificar si ya existe el like
        var existingLike = await _likeRepository.GetLikeAsync(userId, postId);
        if (existingLike != null)
            throw new InvalidOperationException($"El usuario ya ha dado like a este post");

        var like = new Like
        {
            UserId = userId,
            PostId = postId
        };

        _logger.LogInformation("Agregando like: Usuario {UserId} en post {PostId}", userId, postId);
        return await _likeRepository.AddAsync(like);
    }

    public async Task<bool> RemoveLikeAsync(int userId, int postId)
    {
        if (userId <= 0 || postId <= 0)
            return false;

        var like = await _likeRepository.GetLikeAsync(userId, postId);
        if (like == null)
            return false;

        _logger.LogInformation("Removiendo like: Usuario {UserId} del post {PostId}", userId, postId);
        return await _likeRepository.DeleteAsync(like);
    }

    public async Task<bool> DeleteLikeAsync(int likeId)
    {
        var like = await _validationHelper.ValidateLikeExistsAsync(likeId);

        _logger.LogInformation("Eliminando like: {LikeId}", likeId);
        return await _likeRepository.DeleteAsync(likeId);
    }

    // ═══ Toggle (Agregar o Eliminar) ═══

    public async Task<bool> ToggleLikeAsync(int userId, int postId)
    {
        if (userId <= 0 || postId <= 0)
            return false;

        var existingLike = await _likeRepository.GetLikeAsync(userId, postId);

        if (existingLike != null)
        {
            // Si existe, eliminarlo
            _logger.LogInformation("Alternando (removiendo) like: Usuario {UserId} en post {PostId}", userId, postId);
            return await _likeRepository.DeleteAsync(existingLike);
        }
        else
        {
            // Si no existe, crearlo
            try
            {
                await AddLikeAsync(userId, postId);
                _logger.LogInformation("Alternando (agregando) like: Usuario {UserId} en post {PostId}", userId, postId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error al agregar like durante toggle: {Message}", ex.Message);
                return false;
            }
        }
    }

    // ═══ Posts Más Populares ═══

    public async Task<IEnumerable<Post>> GetMostLikedPostsAsync(int take)
    {
        if (take <= 0)
            return Enumerable.Empty<Post>();

        _logger.LogInformation("Obteniendo {Count} posts más populares", take);
        return await _postRepository.GetMostPopularPostsAsync(take);
    }

    public async Task<IEnumerable<Post>> GetMostLikedPostsByUniversityAsync(int universityId, int take)
    {
        if (universityId <= 0 || take <= 0)
            return Enumerable.Empty<Post>();

        _logger.LogInformation("Obteniendo posts más populares de universidad {UniversityId}", universityId);

        var universityPosts = await _postRepository.GetPostsByUniversityIdAsync(universityId);

        // Contar likes para cada post y ordenar
        var postsWithLikes = new List<(Post post, int likeCount)>();

        foreach (var post in universityPosts)
        {
            var likeCount = await _likeRepository.CountLikesByPostAsync(post.Id);
            postsWithLikes.Add((post, likeCount));
        }

        return postsWithLikes
            .OrderByDescending(p => p.likeCount)
            .Take(take)
            .Select(p => p.post);
    }

    // ═══ Usuarios Más Activos ═══

    public async Task<IEnumerable<User>> GetMostActiveUsersAsync(int take)
    {
        if (take <= 0)
            return Enumerable.Empty<User>();

        _logger.LogInformation("Obteniendo {Count} usuarios más activos", take);

        var allLikes = await _likeRepository.GetAllAsync();

        var topLikers = allLikes
            .GroupBy(l => l.UserId)
            .OrderByDescending(g => g.Count())
            .Take(take)
            .Select(g => g.Key);

        var users = new List<User>();
        foreach (var userId in topLikers)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
                users.Add(user);
        }

        return users;
    }

    public async Task<IEnumerable<User>> GetMostActiveUsersByUniversityAsync(int universityId, int take)
    {
        if (universityId <= 0 || take <= 0)
            return Enumerable.Empty<User>();

        _logger.LogInformation("Obteniendo usuarios más activos de universidad {UniversityId}", universityId);

        var universityUsers = await _userRepository.GetUsersByUniversityIdAsync(universityId);
        var universityUserIds = universityUsers.Select(u => u.Id).ToList();

        var allLikes = await _likeRepository.GetAllAsync();

        var universityLikes = allLikes.Where(l => universityUserIds.Contains(l.UserId));

        var topLikers = universityLikes
            .GroupBy(l => l.UserId)
            .OrderByDescending(g => g.Count())
            .Take(take)
            .Select(g => g.Key);

        var users = new List<User>();
        foreach (var userId in topLikers)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
                users.Add(user);
        }

        return users;
    }

    // ═══ Eliminación en Cascada ═══

    public async Task<int> DeleteLikesByPostAsync(int postId)
    {
        if (postId <= 0)
            return 0;

        _logger.LogInformation("Eliminando todos los likes del post {PostId}", postId);
        return await _likeRepository.DeleteLikesByPostAsync(postId);
    }

    // ═══ Likes Recientes ═══

    public async Task<IEnumerable<Like>> GetRecentLikesAsync(int take)
    {
        if (take <= 0)
            return Enumerable.Empty<Like>();

        _logger.LogInformation("Obteniendo {Count} likes recientes", take);

        var allLikes = await _likeRepository.GetAllAsync();

        return allLikes
            .OrderByDescending(l => l.Id)
            .Take(take);
    }

    public async Task<IEnumerable<Like>> GetRecentLikesByUniversityAsync(int universityId, int take)
    {
        if (universityId <= 0 || take <= 0)
            return Enumerable.Empty<Like>();

        _logger.LogInformation("Obteniendo likes recientes de universidad {UniversityId}", universityId);
        return await _likeRepository.GetUniversityLikesAsync(universityId, take);
    }

    // ═══ Estadísticas ═══

    public async Task<int> CountLikesByPostAsync(int postId)
    {
        if (postId <= 0)
            return 0;

        return await _likeRepository.CountLikesByPostAsync(postId);
    }

    public async Task<int> CountLikesByUserAsync(int userId)
    {
        if (userId <= 0)
            return 0;

        var likes = await _likeRepository.GetLikesByUserIdAsync(userId);
        return likes.Count();
    }

    public async Task<LikeStatistics?> GetLikeStatisticsAsync(int? universityId, int days)
    {
        if (days <= 0)
            return null;

        var cutoffDate = DateTime.UtcNow.AddDays(-days);
        var allLikes = await _likeRepository.GetAllAsync();

        IEnumerable<Like> likesPeriod = allLikes;

        if (universityId.HasValue && universityId > 0)
        {
            var universityUsers = await _userRepository.GetUsersByUniversityIdAsync(universityId.Value);
            var universityUserIds = universityUsers.Select(u => u.Id).ToList();
            likesPeriod = allLikes.Where(l => universityUserIds.Contains(l.UserId));
        }

        var totalLikes = likesPeriod.Count();

        if (totalLikes == 0)
            return null;

        var uniquePosts = likesPeriod.Select(l => l.PostId).Distinct().Count();

        var mostLikedPost = likesPeriod
            .GroupBy(l => l.PostId)
            .OrderByDescending(g => g.Count())
            .FirstOrDefault();

        var mostLikesCount = mostLikedPost?.Count() ?? 0;
        var mostLikedPostId = mostLikedPost?.Key ?? 0;

        _logger.LogInformation("Calculando estadísticas de likes para los últimos {Days} días", days);

        return new LikeStatistics
        {
            TotalLikes = totalLikes,
            TotalPosts = uniquePosts,
            AverageLikesPerPost = uniquePosts > 0 ? (double)totalLikes / uniquePosts : 0,
            MostLikedPostId = mostLikedPostId,
            MostLikesCount = mostLikesCount,
            PeriodStart = cutoffDate,
            PeriodEnd = DateTime.UtcNow
        };
    }
}
