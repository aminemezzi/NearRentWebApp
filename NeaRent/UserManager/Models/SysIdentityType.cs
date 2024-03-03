using System;
using System.Collections.Generic;

namespace UserManager.Models;

public partial class SysIdentityType
{
    public int Id { get; set; }

    public string IdentityName { get; set; } = null!;

    public bool Active { get; set; }
}
