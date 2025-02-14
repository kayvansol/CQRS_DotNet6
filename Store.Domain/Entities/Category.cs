using Store.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Store.Domain;

public partial class Category: BaseEntity<int>
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
