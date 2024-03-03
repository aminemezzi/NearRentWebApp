using System;
using System.Collections.Generic;

namespace Products.Models;

public partial class Location
{
    public Guid Id { get; set; }

    public Guid? ParentId { get; set; }

    public bool HasParent { get; set; }

    public int LocationType { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<Location> InverseParent { get; set; } = new List<Location>();

    public virtual LocationType LocationTypeNavigation { get; set; } = null!;

    public virtual Location? Parent { get; set; }

    public virtual ICollection<ProductLocationMap> ProductLocationMaps { get; set; } = new List<ProductLocationMap>();
}
