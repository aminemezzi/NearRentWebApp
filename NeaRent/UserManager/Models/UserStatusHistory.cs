using System;
using System.Collections.Generic;

namespace UserManager.Models;

public partial class UserStatusHistory
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public int StatusId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Note { get; set; }

    public bool Active { get; set; }

    public virtual UserStatus Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
