using System;
using System.Collections.Generic;

namespace Products.Models;

public partial class ProductLocationMap
{
    public Guid LocationId { get; set; }

    public Guid ProductId { get; set; }

    public bool Active { get; set; }

    public virtual Location Location { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
