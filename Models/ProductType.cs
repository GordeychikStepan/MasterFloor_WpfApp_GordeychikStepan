using System;
using System.Collections.Generic;

namespace MasterFloor_WpfApp.Models;

public partial class ProductType
{
    public int ProductTypeId { get; set; }

    public string? ProductType1 { get; set; }

    public decimal? Coefficient { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
