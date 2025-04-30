using System;
using System.Collections.Generic;

namespace MasterFloor_WpfApp.Models;

public partial class PartnerProduct
{
    public int PartnterProductsId { get; set; }

    public int? ProductId { get; set; }

    public int? PartnerId { get; set; }

    public int? ProductCount { get; set; }

    public DateOnly? DateSale { get; set; }

    public virtual Partner? Partner { get; set; }

    public virtual Product? Product { get; set; }
}
