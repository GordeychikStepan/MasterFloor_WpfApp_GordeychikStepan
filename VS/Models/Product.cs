using System;
using System.Collections.Generic;

namespace MasterFloor_WpfApp.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int? ProductTypeId { get; set; }

    public int? ProductMaterialId { get; set; }

    public string? ProductName { get; set; }

    public string? Articul { get; set; }

    public int? MinimumPartnerPrice { get; set; }

    public virtual ICollection<PartnerProduct> PartnerProducts { get; set; } = new List<PartnerProduct>();

    public virtual MaterialType? ProductMaterial { get; set; }

    public virtual ProductType? ProductType { get; set; }
}
