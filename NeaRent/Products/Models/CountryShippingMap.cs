using System;
using System.Collections.Generic;

namespace Products.Models;

public partial class CountryShippingMap
{
    public Guid CountryId { get; set; }

    public int ShippingTypeId { get; set; }

    public decimal Cost { get; set; }

    public bool Active { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ShippingType ShippingType { get; set; } = null!;
}
