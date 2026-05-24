using Microsoft.EntityFrameworkCore;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Repositories;
using UniParche.DataAccess.DbContext;

namespace UniParche.DataAccess.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(UniParcheDbContext context) : base(context)
    {
    }

    // ═══ Búsqueda por Username ═══

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await _dbSet
            .FirstOrDefaultAsync(u => u.UserName == userName);
    }

    // ═══ Búsqueda por Email ═══

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    // ═══ Usuario con Detalles ═══

    public async Task<User?> GetUserWithDetailsAsync(int userId)
    {
        return await _dbSet
            .Include(u => u.University)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    // ═══ Usuarios por Universidad ═══

    public async Task<IEnumerable<User>> GetUsersByUniversityIdAsync(int universityId)
    {
        return await _dbSet
            .Where(u => u.UniversityId == universityId)
            .OrderBy(u => u.UserName)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetUsersByUniversityWithPaginationAsync(
        int universityId, int skip, int take)
    {
        return await _dbSet
            .Where(u => u.UniversityId == universityId)
            .OrderBy(u => u.UserName)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    // ═══ Usuarios por Carrera ═══

    public async Task<IEnumerable<User>> GetUsersByCarrerAsync(int universityId, string carrer)
    {
        return await _dbSet
            .Where(u => u.UniversityId == universityId && u.CareerName == carrer)
            .OrderBy(u => u.UserName)
            .ToListAsync();
    }

    // ═══ Usuarios por Semestre ═══

    public async Task<IEnumerable<User>> GetUsersBySemesterAsync(int universityId, int semester)
    {
        return await _dbSet
            .Where(u => u.UniversityId == universityId && u.Semester == semester)
            .OrderBy(u => u.UserName)
            .ToListAsync();
    }

    // ═══ Búsqueda ═══

    public async Task<IEnumerable<User>> SearchUsersAsync(string query)
    {
        return await _dbSet
            .Where(u => u.UserName.Contains(query) 
                     || u.Email.Contains(query)
                     || u.CareerName.Contains(query))
            .OrderBy(u => u.UserName)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> SearchUsersByUniversityAsync(
        int universityId, string query)
    {
        return await _dbSet
            .Where(u => u.UniversityId == universityId
                     && (u.UserName.Contains(query)
                         || u.Email.Contains(query)
                         || u.CareerName.Contains(query)))
            .OrderBy(u => u.UserName)
            .ToListAsync();
    }

    // ═══ Verificaciones ═══

    public async Task<bool> UserNameExistsAsync(string userName)
    {
        return await _dbSet.AnyAsync(u => u.UserName == userName);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _dbSet.AnyAsync(u => u.Email == email);
    }

    // ═══ Usuarios Activos ═══

    public async Task<IEnumerable<User>> GetMostActiveUsersAsync(int universityId, int take)
    {
        // Ordenado por fecha de registro (más recientes primero)
        return await _dbSet
            .Where(u => u.UniversityId == universityId)
            .OrderByDescending(u => u.RegisterTime)
            .Take(take)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetRecentlyRegisteredUsersAsync(int universityId, int take)
    {
        return await _dbSet
            .Where(u => u.UniversityId == universityId)
            .OrderByDescending(u => u.RegisterTime)
            .Take(take)
            .ToListAsync();
    }

    // ═══ Conteos ═══

    public async Task<int> CountUsersByUniversityAsync(int universityId)
    {
        return await _dbSet.CountAsync(u => u.UniversityId == universityId);
    }

    public async Task<int> CountUsersByCarrerAsync(int universityId, string carrer)
    {
        return await _dbSet.CountAsync(u => u.UniversityId == universityId && u.CareerName == carrer);
    }

    // ═══ Sugerencias ═══

    public async Task<IEnumerable<User>> GetSuggestedUsersAsync(int userId, int take)
    {
        var user = await GetByIdAsync(userId);
        if (user == null)
            return Enumerable.Empty<User>();

        return await _dbSet
            .Where(u => u.UniversityId == user.UniversityId
                     && u.Id != userId
                     && u.CareerName == user.CareerName)
            .OrderBy(u => u.UserName)
            .Take(take)
            .ToListAsync();
    }

    // ═══ Estadísticas ═══

    public async Task<UserStatistics?> GetUserStatisticsAsync(int userId)
    {
        var user = await _dbSet
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return null;

        var postCount = await _context.Posts.CountAsync(p => p.UserId == userId);
        var commentCount = await _context.Comments.CountAsync(c => c.UserId == userId);
        var likeCount = await _context.Likes.CountAsync(l => l.UserId == userId);
        var receivedLikesCount = await _context.Likes
            .CountAsync(l => l.Post.UserId == userId);

        return new UserStatistics
        {
            UserId = userId,
            UserName = user.UserName,
            TotalPosts = postCount,
            TotalComments = commentCount,
            TotalLikesGiven = likeCount,
            TotalLikesReceived = receivedLikesCount,
            RegisterDate = user.RegisterTime
        };
    }
}
