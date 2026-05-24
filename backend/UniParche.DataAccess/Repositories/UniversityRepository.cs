using Microsoft.EntityFrameworkCore;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Repositories;
using UniParche.DataAccess.DbContext;

namespace UniParche.DataAccess.Repositories;

public class UniversityRepository : GenericRepository<University>, IUniversityRepository
{
    public UniversityRepository(UniParcheDbContext context) : base(context)
    {
    }

    // ═══ Búsqueda por Email de Dominio ═══

    public async Task<University?> GetByDomainEmailAsync(string domainEmail)
    {
        return await _dbSet
            .FirstOrDefaultAsync(u => u.DomainEmail == domainEmail);
    }

    // ═══ Universidades con Relaciones ═══

    public async Task<University?> GetUniversityWithUsersAsync(int universityId)
    {
        return await _dbSet
            .Include(u => u.Users)
            .FirstOrDefaultAsync(u => u.Id == universityId);
    }

    public async Task<University?> GetUniversityWithPostsAsync(int universityId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(u => u.Id == universityId);
    }

    // ═══ Universidades con Conteos ═══

    public async Task<IEnumerable<University>> GetUniversitiesWithUserCountAsync()
    {
        return await _dbSet
            .Include(u => u.Users)
            .ToListAsync();
    }

    // ═══ Búsqueda ═══

    public async Task<IEnumerable<University>> SearchUniversitiesAsync(string query)
    {
        return await _dbSet
            .Where(u => u.Name.Contains(query) || u.DomainEmail.Contains(query))
            .ToListAsync();
    }

    // ═══ Conteos ═══

    public async Task<int> CountUsersByUniversityAsync(int universityId)
    {
        return await _dbSet
            .Where(u => u.Id == universityId)
            .SelectMany(u => u.Users)
            .CountAsync();
    }

    public async Task<int> CountPostsByUniversityAsync(int universityId)
    {
        var university = await _dbSet
            .FirstOrDefaultAsync(u => u.Id == universityId);

        if (university == null)
            return 0;

        // Contar posts de usuarios de esta universidad
        return await _context.Posts
            .CountAsync(p => p.User.UniversityId == universityId);
    }

    // ═══ Universidades Activas/Trending ═══

    public async Task<IEnumerable<University>> GetMostActiveUniversitiesAsync(int count)
    {
        return await _dbSet
            .Include(u => u.Users)
            .OrderByDescending(u => u.Users.Count)
            .Take(count)
            .ToListAsync();
    }

    public async Task<IEnumerable<University>> GetUniversitiesWithMostUsersAsync(int count)
    {
        return await _dbSet
            .Include(u => u.Users)
            .OrderByDescending(u => u.Users.Count)
            .Take(count)
            .ToListAsync();
    }

    // ═══ Verificaciones ═══

    public async Task<bool> UniversityExistsAsync(int universityId)
    {
        return await _dbSet.AnyAsync(u => u.Id == universityId);
    }

    public async Task<bool> UniversityExistsByDomainAsync(string domainEmail)
    {
        return await _dbSet.AnyAsync(u => u.DomainEmail == domainEmail);
    }

    public async Task<bool> UniversityExistsByNameAsync(string name)
    {
        return await _dbSet.AnyAsync(u => u.Name == name);
    }

    // ═══ Listado con Paginación ═══

    public async Task<IEnumerable<University>> GetUniversitiesWithPaginationAsync(
        int pageNumber, int pageSize)
    {
        return await _dbSet
            .OrderBy(u => u.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    // ═══ Estadísticas ═══

    public async Task<UniversityStatistics?> GetUniversityStatisticsAsync(int universityId)
    {
        var university = await _dbSet
            .Include(u => u.Users)
            .FirstOrDefaultAsync(u => u.Id == universityId);

        if (university == null)
            return null;

        var userCount = university.Users.Count;
        var postCount = await _context.Posts
            .CountAsync(p => p.User.UniversityId == universityId);
        var commentCount = await _context.Comments
            .CountAsync(c => c.User.UniversityId == universityId);
        var likeCount = await _context.Likes
            .CountAsync(l => l.User.UniversityId == universityId);

        return new UniversityStatistics
        {
            UniversityId = universityId,
            UniversityName = university.Name,
            TotalUsers = userCount,
            TotalPosts = postCount,
            TotalComments = commentCount,
            TotalLikes = likeCount
        };
    }
}
