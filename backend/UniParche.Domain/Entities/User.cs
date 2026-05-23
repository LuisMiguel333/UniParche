using System;
using System.Collections.Generic;
using System.Text;

namespace UniParche.Domain.Entities;

public class User: AuditBase
{
    public string user_name { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string password_hash { get; set; } = string.Empty;
    public string profile_picture_url { get; set; } = string.Empty;
    public string Carrer_name { get; set; } = string.Empty;
    public int semester { get; set; }
    public DateTime register_time { get; set; }

    // Foreign key
    public int UniversityId { get; set; }

    // navigation properties
    public University University { get; set; } = null!;

}
