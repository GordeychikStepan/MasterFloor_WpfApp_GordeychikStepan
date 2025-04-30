using System;
using System.Collections.Generic;

namespace MasterFloor_WpfApp.Models;

public partial class MaterialType
{
    public int MaterialTypeId { get; set; }

    public string? MaterialType1 { get; set; }

    public decimal? PercentDamage { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
