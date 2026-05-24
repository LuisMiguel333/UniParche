using System;
using System.Collections.Generic;
using System.Text;

namespace UniParche.Domain.Entities;

public class University : AuditBase
{
    public string Name { get; set; } = string.Empty;
    public string DomainEmail { get; set; } = string.Empty;

    // navigation properties
    public ICollection<User> Users { get; set; } = new List<User>();
}
