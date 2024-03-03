using System;
using System.Collections.Generic;

namespace UserManager.Models;

public partial class UserStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<UserStatusHistory> UserStatusHistories { get; set; } = new List<UserStatusHistory>();
}
