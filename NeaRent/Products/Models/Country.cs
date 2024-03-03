using System;
using System.Collections.Generic;

namespace Products.Models;

public partial class Country
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Active { get; set; }

    public virtual ICollection<CountryShippingMap> CountryShippingMaps { get; set; } = new List<CountryShippingMap>();
}
