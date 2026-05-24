using Microsoft.EntityFrameworkCore;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Repositories;
using UniParche.DataAccess.DbContext;

namespace UniParche.DataAccess.Repositories;

public class LikeRepository : GenericRepository<Like>, ILikeRepository
{
    public LikeRepository(UniParcheDbContext context) : base(context)
    {
    }

    // ═══ Likes por Post ═══

    public async Task<IEnumerable<Like>> GetLikesByPostIdAsync(int postId)
    {
        return await _dbSet
            .Where(l => l.PostId == postId)
            .OrderByDescending(l => l.Id)
            .ToListAsync();
    }

    public async Task<IEnumerable<Like>> GetLikesWithUserAsync(int postId)
    {
        return await _dbSet
            .Where(l => l.PostId == postId)
            .Include(l => l.User)
            .OrderByDescending(l => l.Id)
            .ToListAsync();
    }

    public async Task<IEnumerable<Like>> GetLikesByPostWithPaginationAsync(
        int postId, int pageNumber, int pageSize)
    {
        return await _dbSet
            .Where(l => l.PostId == postId)
            .Include(l => l.User)
            .OrderByDescending(l => l.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    // ═══ Likes por Usuario ═══

    public async Task<IEnumerable<Like>> GetLikesByUserIdAsync(int userId)
    {
        return await _dbSet
            .Where(l => l.UserId == userId)
            .OrderByDescending(l => l.Id)
            .ToListAsync();
    }

    // ═══ Verificaciones ═══

    public async Task<bool> HasUserLikedPostAsync(int userId, int postId)
    {
        return await _dbSet.AnyAsync(l => l.UserId == userId && l.PostId == postId);
    }

    public async Task<Like?> GetLikeAsync(int userId, int postId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(l => l.UserId == userId && l.PostId == postId);
    }

    // ═══ Conteos ═══

    public async Task<int> CountLikesByPostAsync(int postId)
    {
        return await _dbSet.CountAsync(l => l.PostId == postId);
    }

    // ═══ Posts Más Likeados ═══

    public async Task<IEnumerable<Like>> GetMostLikedPostsAsync(int? universityId, int take)
    {
        var query = _dbSet.AsQueryable();

        if (universityId.HasValue)
        {
            query = query.Where(l => l.User.UniversityId == universityId.Value);
        }

        var likesByPost = await query
            .GroupBy(l => l.PostId)
            .OrderByDescending(g => g.Count())
            .Take(take)
            .SelectMany(g => g)
            .Include(l => l.Post)
            .ToListAsync();

        return likesByPost;
    }

    // ═══ Eliminación ═══

    public async Task<int> DeleteLikesByPostAsync(int postId)
    {
        var likes = await _dbSet.Where(l => l.PostId == postId).ToListAsync();
        _dbSet.RemoveRange(likes);
        await _context.SaveChangesAsync();
        return likes.Count;
    }

    // ═══ Likes por Universidad ═══

    public async Task<IEnumerable<Like>> GetUniversityLikesAsync(
        int universityId, int count)
    {
        return await _dbSet
            .Include(l => l.User)
            .Include(l => l.Post)
            .Where(l => l.User.UniversityId == universityId)
            .OrderByDescending(l => l.Id)
            .Take(count)
            .ToListAsync();
    }
}
