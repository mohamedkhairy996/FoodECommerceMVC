using System;
using System.Collections.Generic;

namespace DomainLayerCore.Models
{
    public partial class EcommerceAccount
    {
        public int Id { get; set; }
        public string AccountIdNum { get; set; } = null!;
        public double? TotalMoney { get; set; }
    }
}
