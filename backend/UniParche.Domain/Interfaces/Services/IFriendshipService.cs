using UniParche.Domain.Entities;
using UniParche.Domain.Enums;

namespace UniParche.Domain.Interfaces.Services;

/// <summary>
/// Contrato del servicio de amistades
/// </summary>
public interface IFriendshipService
{
	// ═══ Consultas ═══
	Task<IEnumerable<Friendship>> GetFriendsByUserAsync(int userId);
	Task<Friendship?> GetFriendshipAsync(int user1Id, int user2Id);
	Task<IEnumerable<Friendship>> GetPendingRequestsAsync(int userId);

	// ═══ Acciones ═══
	Task<Friendship> SendRequestAsync(int user1Id, int user2Id);
	Task<Friendship> UpdateStatusAsync(int user1Id, int user2Id, FriendshipStatus status);
	Task<bool> DeleteFriendshipAsync(int user1Id, int user2Id);
}