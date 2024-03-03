using System;
using System.Collections.Generic;

namespace UserManager.Models;

public partial class UserAddress
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public int AddressTypeId { get; set; }

    public Guid AddressStreetId { get; set; }

    public bool Active { get; set; }

    public virtual ContactTypeAddress AddressType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
