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
/// Implementación del servicio de comentarios con lógica de negocio
/// </summary>
public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly PostValidationHelper _validationHelper;
    private readonly ILogger<CommentService> _logger;

    public CommentService(
        ICommentRepository commentRepository,
        PostValidationHelper validationHelper,
        ILogger<CommentService> logger)
    {
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        _validationHelper = validationHelper ?? throw new ArgumentNullException(nameof(validationHelper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    // ═══ Obtención de Comentarios ═══

    public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
    {
        _logger.LogInformation("Obteniendo todos los comentarios");
        return await _commentRepository.GetAllAsync();
    }

    public async Task<Comment?> GetCommentByIdAsync(int commentId)
    {
        if (commentId <= 0)
            return null;

        _logger.LogInformation("Obteniendo comentario con ID {CommentId}", commentId);
        return await _commentRepository.GetByIdAsync(commentId);
    }

    public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        if (postId <= 0)
            return Enumerable.Empty<Comment>();

        await _validationHelper.ValidatePostExistsAsync(postId);
        _logger.LogInformation("Obteniendo comentarios del post {PostId}", postId);
        return await _commentRepository.GetCommentsByPostIdAsync(postId);
    }

    public async Task<(IEnumerable<Comment> comments, int totalCount)> GetCommentsByPostPaginatedAsync(int postId, int pageNumber, int pageSize)
    {
        PostValidationHelper.ValidatePagination(pageNumber, pageSize);
        await _validationHelper.ValidatePostExistsAsync(postId);

        _logger.LogInformation("Obteniendo comentarios del post {PostId} - Página {Page}", postId, pageNumber);

        var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);
        var totalCount = comments.Count();

        var paginatedComments = comments
            .OrderByDescending(c => c.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return (paginatedComments, totalCount);
    }

    public async Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(int userId)
    {
        if (userId <= 0)
            return Enumerable.Empty<Comment>();

        await _validationHelper.ValidateUserExistsAsync(userId);
        _logger.LogInformation("Obteniendo comentarios del usuario {UserId}", userId);
        return await _commentRepository.GetCommentsByUserIdAsync(userId);
    }

    public async Task<IEnumerable<Comment>> GetCommentsByPostWithUserAsync(int postId)
    {
        await _validationHelper.ValidatePostExistsAsync(postId);
        _logger.LogInformation("Obteniendo comentarios con usuario del post {PostId}", postId);
        return await _commentRepository.GetCommentsByPostWithUserAsync(postId);
    }

    // ═══ Crear, Actualizar y Eliminar Comentarios ═══

    public async Task<Comment> CreateCommentAsync(Comment comment)
    {
        if (comment == null)
            throw new ArgumentNullException(nameof(comment));

        if (comment.UserId <= 0)
            throw new ArgumentException("El ID del usuario es requerido", nameof(comment.UserId));

        if (comment.PostId <= 0)
            throw new ArgumentException("El ID del post es requerido", nameof(comment.PostId));

        // Validaciones
        PostValidationHelper.ValidateCommentContent(comment.Content);
        await _validationHelper.ValidateUserExistsAsync(comment.UserId);
        await _validationHelper.ValidatePostExistsAsync(comment.PostId);

        comment.CreatedAt = DateTime.UtcNow;
        comment.UpdatedAt = DateTime.UtcNow;

        _logger.LogInformation("Creando comentario en post {PostId} del usuario {UserId}", comment.PostId, comment.UserId);
        return await _commentRepository.AddAsync(comment);
    }

    public async Task<Comment> UpdateCommentAsync(Comment comment)
    {
        if (comment == null)
            throw new ArgumentNullException(nameof(comment));

        if (comment.Id <= 0)
            throw new ArgumentException("El comentario debe tener un ID válido", nameof(comment.Id));

        var existingComment = await _validationHelper.ValidateCommentExistsAsync(comment.Id);
        PostValidationHelper.ValidateCommentContent(comment.Content);

        comment.UpdatedAt = DateTime.UtcNow;

        _logger.LogInformation("Actualizando comentario: {CommentId}", comment.Id);
        return await _commentRepository.UpdateAsync(comment);
    }

    public async Task<bool> DeleteCommentAsync(int commentId)
    {
        var comment = await _validationHelper.ValidateCommentExistsAsync(commentId);

        _logger.LogInformation("Eliminando comentario: {CommentId}", commentId);
        return await _commentRepository.DeleteAsync(commentId);
    }

    // ═══ Consultas Especializadas ═══

    public async Task<bool> HasCommentedAsync(int userId, int postId)
    {
        if (userId <= 0 || postId <= 0)
            return false;

        return await _commentRepository.HasCommentedAsync(userId, postId);
    }

    public async Task<IEnumerable<Comment>> GetRecentCommentsByUniversityAsync(int universityId, int take)
    {
        if (universityId <= 0 || take <= 0)
            return Enumerable.Empty<Comment>();

        _logger.LogInformation("Obteniendo comentarios recientes de universidad {UniversityId}", universityId);
        return await _commentRepository.GetRecentCommentsByUniversityAsync(universityId, take);
    }

    public async Task<IEnumerable<Comment>> GetRecentCommentsAsync(int take)
    {
        if (take <= 0)
            return Enumerable.Empty<Comment>();

        _logger.LogInformation("Obteniendo {Count} comentarios recientes", take);

        var allComments = await _commentRepository.GetAllAsync();

        return allComments
            .OrderByDescending(c => c.CreatedAt)
            .Take(take);
    }

    public async Task<IEnumerable<Comment>> SearchCommentsAsync(string searchTerm)
    {
        PostValidationHelper.ValidateSearchTerm(searchTerm);
        _logger.LogInformation("Buscando comentarios: {SearchTerm}", searchTerm);

        var allComments = await _commentRepository.GetAllAsync();

        return allComments
            .Where(c => c.Content.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<IEnumerable<User>> GetMostActiveCommentersAsync(int postId, int take)
    {
        if (take <= 0)
            return Enumerable.Empty<User>();

        await _validationHelper.ValidatePostExistsAsync(postId);

        _logger.LogInformation("Obteniendo usuarios más activos comentando en post {PostId}", postId);

        var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);

        var topCommenters = comments
            .GroupBy(c => c.UserId)
            .OrderByDescending(g => g.Count())
            .Take(take)
            .Select(g => g.Key);

        var users = new List<User>();
        foreach (var userId in topCommenters)
        {
            var user = await _validationHelper.ValidateUserExistsAsync(userId);
            if (user != null)
                users.Add(user);
        }

        return users;
    }

    // ═══ Eliminación en Cascada ═══

    public async Task<int> DeleteCommentsByPostAsync(int postId)
    {
        await _validationHelper.ValidatePostExistsAsync(postId);

        var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);
        var deletedCount = 0;

        _logger.LogInformation("Eliminando todos los comentarios del post {PostId}", postId);

        foreach (var comment in comments)
        {
            if (await _commentRepository.DeleteAsync(comment))
                deletedCount++;
        }

        _logger.LogInformation("Se eliminaron {Count} comentarios del post {PostId}", deletedCount, postId);
        return deletedCount;
    }

    public async Task<int> DeleteCommentsByUserAsync(int userId)
    {
        await _validationHelper.ValidateUserExistsAsync(userId);

        var comments = await _commentRepository.GetCommentsByUserIdAsync(userId);
        var deletedCount = 0;

        _logger.LogInformation("Eliminando todos los comentarios del usuario {UserId}", userId);

        foreach (var comment in comments)
        {
            if (await _commentRepository.DeleteAsync(comment))
                deletedCount++;
        }

        _logger.LogInformation("Se eliminaron {Count} comentarios del usuario {UserId}", deletedCount, userId);
        return deletedCount;
    }

    // ═══ Estadísticas ═══

    public async Task<int> CountCommentsByPostAsync(int postId)
    {
        if (postId <= 0)
            return 0;

        return await _commentRepository.CountCommentsByPostAsync(postId);
    }

    public async Task<int> CountCommentsByUserAsync(int userId)
    {
        if (userId <= 0)
            return 0;

        var comments = await _commentRepository.GetCommentsByUserIdAsync(userId);
        return comments.Count();
    }

    public async Task<IEnumerable<Comment>> GetMostLikedCommentsAsync(int postId, int take)
    {
        if (take <= 0)
            return Enumerable.Empty<Comment>();

        await _validationHelper.ValidatePostExistsAsync(postId);

        _logger.LogInformation("Obteniendo comentarios más populares del post {PostId}", postId);

        var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);

        return comments
            .OrderByDescending(c => c.CreatedAt)
            .Take(take);
    }
}
