using System;
using System.Collections.Generic;

namespace Products.Models;

public partial class Product
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime ListDate { get; set; }

    public DateTime? ListFromDate { get; set; }

    public DateTime? ListToDate { get; set; }

    public DateTime? CancelDate { get; set; }

    public int? ReservationType { get; set; }

    public double? ReservationValue { get; set; }

    public string? ImageName { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<ProductCategoryMap> ProductCategoryMaps { get; set; } = new List<ProductCategoryMap>();

    public virtual ICollection<ProductLocationMap> ProductLocationMaps { get; set; } = new List<ProductLocationMap>();

    public virtual ICollection<ProductRating> ProductRatings { get; set; } = new List<ProductRating>();

    public virtual ICollection<ProductRentalMap> ProductRentalMaps { get; set; } = new List<ProductRentalMap>();

    public virtual ICollection<ProductReservation> ProductReservations { get; set; } = new List<ProductReservation>();

    public virtual ICollection<ProductShippingMap> ProductShippingMaps { get; set; } = new List<ProductShippingMap>();

    public virtual ReservationType? ReservationTypeNavigation { get; set; }

    public virtual UserStub User { get; set; } = null!;
}
