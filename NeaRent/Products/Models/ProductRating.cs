using System;
using System.Collections.Generic;

namespace Products.Models;

public partial class ProductRating
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public Guid UserId { get; set; }

    public int Rating { get; set; }

    public string? Review { get; set; }

    public bool Active { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual UserStub User { get; set; } = null!;
}
