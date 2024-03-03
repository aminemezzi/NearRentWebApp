using System;
using System.Collections.Generic;

namespace Products.Models;

public partial class ProductShippingMap
{
    public Guid ProductId { get; set; }

    public int ShippingTypeId { get; set; }

    public bool Active { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ShippingType ShippingType { get; set; } = null!;
}
