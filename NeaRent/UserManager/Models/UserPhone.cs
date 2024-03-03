using System;
using System.Collections.Generic;

namespace UserManager.Models;

public partial class UserPhone
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public int PhoneTypeId { get; set; }

    public int CountryCode { get; set; }

    public string Number { get; set; } = null!;

    public bool Active { get; set; }

    public virtual ContactTypePhone PhoneType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
