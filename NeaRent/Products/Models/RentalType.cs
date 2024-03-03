using System;
using System.Collections.Generic;

namespace Products.Models;

public partial class RentalType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<ProductRentalMap> ProductRentalMaps { get; set; } = new List<ProductRentalMap>();
}
