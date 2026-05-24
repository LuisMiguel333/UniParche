using System.ComponentModel.DataAnnotations;
using UniParche.Domain.Enums;

namespace UniParche.API.DTOs.Request;

public class UpdateFriendshipRequest
{
    [Required]
    public FriendshipStatus Status { get; set; }
}