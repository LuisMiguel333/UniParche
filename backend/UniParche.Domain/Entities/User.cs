namespace UniParche.Domain.Entities;

public class User : AuditBase
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string ProfilePictureUrl { get; set; } = string.Empty;
    public string CareerName { get; set; } = string.Empty;
    public int Semester { get; set; }
    public DateTime RegisterTime { get; set; }

    // Foreign key
    public int UniversityId { get; set; }

    // Navigation properties
    public University University { get; set; } = null!;
}
