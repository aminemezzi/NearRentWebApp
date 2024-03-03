using System;
using System.Collections.Generic;

namespace Products.Models;

public partial class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool HasChildren { get; set; }

    public Guid? ParentId { get; set; }

    public bool IsFeatured { get; set; }

    public string? ImageName { get; set; }

    public string? Text { get; set; }

    public int SortOrder { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<Category> InverseParent { get; set; } = new List<Category>();

    public virtual Category? Parent { get; set; }

    public virtual ICollection<ProductCategoryMap> ProductCategoryMaps { get; set; } = new List<ProductCategoryMap>();
}
