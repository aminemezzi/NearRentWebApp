using System;
using System.Collections.Generic;

namespace Products.Models;

public partial class ProductCategoryMap
{
    public Guid CategoryId { get; set; }

    public Guid ProductId { get; set; }

    public bool Active { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
