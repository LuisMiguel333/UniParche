using System.ComponentModel.DataAnnotations;

namespace UniParche.API.DTOs.Request;

public class CreateFriendshipRequest
{
	[Required]
	public int User1Id { get; set; }

	[Required]
	public int User2Id { get; set; }
}