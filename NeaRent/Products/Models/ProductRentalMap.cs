using System;
using System.Collections.Generic;

namespace Products.Models;

public partial class ProductRentalMap
{
    public Guid ProductId { get; set; }

    public int RentalTypeId { get; set; }

    public decimal Price { get; set; }

    public bool Active { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual RentalType RentalType { get; set; } = null!;
}
