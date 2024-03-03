using System;
using System.Collections.Generic;

namespace Products.Models;

public partial class UserStub
{
    public Guid Id { get; set; }

    public DateTime CreateDate { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<ProductRating> ProductRatings { get; set; } = new List<ProductRating>();

    public virtual ICollection<ProductReservation> ProductReservations { get; set; } = new List<ProductReservation>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
