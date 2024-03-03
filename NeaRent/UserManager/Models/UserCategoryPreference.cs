using System;
using System.Collections.Generic;

namespace UserManager.Models;

public partial class UserCategoryPreference
{
    public Guid Id { get; set; }

    public Guid AzureObjectId { get; set; }

    public Guid CategoryId { get; set; }

    public virtual User AzureObject { get; set; } = null!;
}
