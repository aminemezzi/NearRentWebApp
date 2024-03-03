using System;
using System.Collections.Generic;

namespace UserManager.Models;

public partial class SysUserType
{
    public int Id { get; set; }

    public string TypeName { get; set; } = null!;

    public bool Activer { get; set; }
}
