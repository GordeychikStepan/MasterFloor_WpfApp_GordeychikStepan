using System;
using System.Collections.Generic;

namespace MasterFloor_WpfApp.Models;

public partial class PartnerType
{
    public int PartnerTypeId { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<Partner> Partners { get; set; } = new List<Partner>();
}
