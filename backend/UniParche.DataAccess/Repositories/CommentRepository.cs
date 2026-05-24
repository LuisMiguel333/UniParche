using Microsoft.EntityFrameworkCore;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Repositories;
using UniParche.DataAccess.DbContext;

namespace UniParche.DataAccess.Repositories;

public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(UniParcheDbContext context) : base(context)
    {
    }

    // ═══ Comentarios por Post ═══

    public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        return await _dbSet
            .Where(c => c.PostId == postId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Comment>> GetCommentsByPostWithPaginationAsync(
        int postId, int pageNumber, int pageSize)
    {
        return await _dbSet
            .Where(c => c.PostId == postId)
            .OrderByDescending(c => c.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(c => c.User)
            .ToListAsync();
    }

    public async Task<int> CountCommentsByPostAsync(int postId)
    {
        return await _dbSet.CountAsync(c => c.PostId == postId);
    }

    // ═══ Comentarios por Usuario ═══

    public async Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(int userId)
    {
        return await _dbSet
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    // ═══ Comentarios con Detalles ═══

    public async Task<IEnumerable<Comment>> GetCommentsByPostWithUserAsync(int postId)
    {
        return await _dbSet
            .Where(c => c.PostId == postId)
            .Include(c => c.User)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    // ═══ Verificaciones ═══

    public async Task<bool> HasCommentedAsync(int userId, int postId)
    {
        return await _dbSet.AnyAsync(c => c.UserId == userId && c.PostId == postId);
    }

    // ═══ Comentarios Recientes por Universidad ═══

    public async Task<IEnumerable<Comment>> GetRecentCommentsByUniversityAsync(
        int universityId, int count)
    {
        return await _dbSet
            .Include(c => c.User)
            .Include(c => c.Post)
            .Where(c => c.User.UniversityId == universityId)
            .OrderByDescending(c => c.CreatedAt)
            .Take(count)
            .ToListAsync();
    }
}
