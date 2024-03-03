using System;
using System.Collections.Generic;

namespace UserManager.Models;

public partial class UserEmail
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public int EmailType { get; set; }

    public string Email { get; set; } = null!;

    public bool Verified { get; set; }

    public bool Active { get; set; }

    public virtual ContactTypeEmail EmailTypeNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
