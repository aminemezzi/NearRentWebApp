using System;
using System.Collections.Generic;

namespace Products.Models;

public partial class ShippingType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<CountryShippingMap> CountryShippingMaps { get; set; } = new List<CountryShippingMap>();

    public virtual ICollection<ProductShippingMap> ProductShippingMaps { get; set; } = new List<ProductShippingMap>();
}
