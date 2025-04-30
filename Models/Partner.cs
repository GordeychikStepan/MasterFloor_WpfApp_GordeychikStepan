using System;
using System.Collections.Generic;

namespace MasterFloor_WpfApp.Models;

public partial class Partner
{
    public int PartnerId { get; set; }

    public int? PartnerTypeId { get; set; }

    public string? PartnerName { get; set; }

    public string? Ceo { get; set; }

    public string? PartnerEmail { get; set; }

    public string? PartnerPhone { get; set; }

    public string? PartnerAddress { get; set; }

    public string? Inn { get; set; }

    public int? Rate { get; set; }

    public virtual ICollection<PartnerProduct> PartnerProducts { get; set; } = new List<PartnerProduct>();

    public virtual PartnerType? PartnerType { get; set; }
}
