using Microsoft.Extensions.Logging;
using UniParche.Domain.Entities;
using UniParche.Domain.Enums;
using UniParche.Domain.Interfaces.Repositories;
using UniParche.Domain.Interfaces.Services;

namespace UniParche.Domain.Services;

/// <summary>
/// Implementación del servicio de amistades
/// </summary>
public class FriendshipService : IFriendshipService
{
    private readonly IGenericRepository<Friendship> _friendshipRepository;
    private readonly ILogger<FriendshipService> _logger;

    public FriendshipService(
        IGenericRepository<Friendship> friendshipRepository,
        ILogger<FriendshipService> logger)
    {
        _friendshipRepository = friendshipRepository ?? throw new ArgumentNullException(nameof(friendshipRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    // ═══ Consultas ═══

    public async Task<IEnumerable<Friendship>> GetFriendsByUserAsync(int userId)
    {
        if (userId <= 0) return Enumerable.Empty<Friendship>();

        _logger.LogInformation("Obteniendo amigos del usuario {UserId}", userId);
        return await _friendshipRepository.GetByExpressionAsync(f =>
            (f.User1Id == userId || f.User2Id == userId) &&
            f.Status == FriendshipStatus.Accepted);
    }

    public async Task<Friendship?> GetFriendshipAsync(int user1Id, int user2Id)
    {
        _logger.LogInformation("Buscando amistad entre {User1Id} y {User2Id}", user1Id, user2Id);
        return await _friendshipRepository.FirstOrDefaultAsync(f =>
            (f.User1Id == user1Id && f.User2Id == user2Id) ||
            (f.User1Id == user2Id && f.User2Id == user1Id));
    }

    public async Task<IEnumerable<Friendship>> GetPendingRequestsAsync(int userId)
    {
        if (userId <= 0) return Enumerable.Empty<Friendship>();

        _logger.LogInformation("Obteniendo solicitudes pendientes del usuario {UserId}", userId);
        return await _friendshipRepository.GetByExpressionAsync(f =>
            f.User2Id == userId &&
            f.Status == FriendshipStatus.Pending);
    }

    // ═══ Acciones ═══

    public async Task<Friendship> SendRequestAsync(int user1Id, int user2Id)
    {
        if (user1Id == user2Id)
            throw new ArgumentException("No puedes enviarte una solicitud a ti mismo.");

        var existing = await GetFriendshipAsync(user1Id, user2Id);
        if (existing != null)
            throw new InvalidOperationException("Ya existe una solicitud o amistad entre estos usuarios.");

        var friendship = new Friendship
        {
            User1Id = user1Id,
            User2Id = user2Id,
            Status = FriendshipStatus.Pending,
            Date = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _logger.LogInformation("Usuario {User1Id} envió solicitud a {User2Id}", user1Id, user2Id);
        return await _friendshipRepository.AddAsync(friendship);
    }

    public async Task<Friendship> UpdateStatusAsync(int user1Id, int user2Id, FriendshipStatus status)
    {
        var friendship = await GetFriendshipAsync(user1Id, user2Id)
            ?? throw new KeyNotFoundException("No se encontró la solicitud de amistad.");

        friendship.Status = status;
        friendship.UpdatedAt = DateTime.UtcNow;

        _logger.LogInformation("Amistad entre {User1Id} y {User2Id} actualizada a {Status}", user1Id, user2Id, status);
        return await _friendshipRepository.UpdateAsync(friendship);
    }

    public async Task<bool> DeleteFriendshipAsync(int user1Id, int user2Id)
    {
        var friendship = await GetFriendshipAsync(user1Id, user2Id)
            ?? throw new KeyNotFoundException("No se encontró la amistad.");

        _logger.LogInformation("Eliminando amistad entre {User1Id} y {User2Id}", user1Id, user2Id);
        return await _friendshipRepository.DeleteAsync(friendship);
    }
}