using Microsoft.EntityFrameworkCore;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Repositories;
using UniParche.DataAccess.DbContext;

namespace UniParche.DataAccess.Repositories;

public class PostRepository : GenericRepository<Post>, IPostRepository
{
    public PostRepository(UniParcheDbContext context) : base(context)
    {
    }

    // ═══ Posts por Usuario ═══

    public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId)
    {
        return await _dbSet
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    // ═══ Posts por Universidad ═══

    public async Task<IEnumerable<Post>> GetPostsByUniversityIdAsync(int universityId)
    {
        return await _dbSet
            .Include(p => p.User)
            .Where(p => p.User.UniversityId == universityId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    // ═══ Posts Recientes ═══

    public async Task<IEnumerable<Post>> GetRecentPostsAsync(int skip, int take)
    {
        return await _dbSet
            .OrderByDescending(p => p.CreatedAt)
            .Skip(skip)
            .Take(take)
            .Include(p => p.User)
            .ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetRecentUniversityPostsAsync(
        int universityId, int skip, int take)
    {
        return await _dbSet
            .Include(p => p.User)
            .Where(p => p.User.UniversityId == universityId)
            .OrderByDescending(p => p.CreatedAt)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    // ═══ Posts con Detalles ═══

    public async Task<Post?> GetPostWithDetailsAsync(int postId)
    {
        return await _dbSet
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == postId);
    }

    public async Task<IEnumerable<Post>> GetPostsByUserWithDetailsAsync(int userId)
    {
        return await _dbSet
            .Where(p => p.UserId == userId)
            .Include(p => p.User)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    // ═══ Búsqueda ═══

    public async Task<IEnumerable<Post>> SearchPostsAsync(string query)
    {
        return await _dbSet
            .Where(p => p.Title.Contains(query) || p.Content.Contains(query))
            .OrderByDescending(p => p.CreatedAt)
            .Include(p => p.User)
            .ToListAsync();
    }

    // ═══ Posts Populares ═══

    public async Task<IEnumerable<Post>> GetMostPopularPostsAsync(int count)
    {
        // Esto requeriría un conteo de likes/comentarios
        // Por ahora, retorna los posts más recientes
        return await _dbSet
            .OrderByDescending(p => p.CreatedAt)
            .Take(count)
            .ToListAsync();
    }

    // ═══ Feed Universitario ═══

    public async Task<IEnumerable<Post>> GetUniversityFeedAsync(
        int universityId, int pageNumber, int pageSize)
    {
        return await _dbSet
            .Include(p => p.User)
            .Where(p => p.User.UniversityId == universityId)
            .OrderByDescending(p => p.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    // ═══ Conteos ═══

    public async Task<int> CountPostsByUserAsync(int userId)
    {
        return await _dbSet.CountAsync(p => p.UserId == userId);
    }

    public async Task<int> CountPostsByUniversityAsync(int universityId)
    {
        return await _dbSet
            .Include(p => p.User)
            .CountAsync(p => p.User.UniversityId == universityId);
    }
}
