using System;
using System.Collections.Generic;

namespace DomainLayerCore.Models
{
    public partial class Transiaction
    {
        public int Id { get; set; }
        public string? AccountIdNum { get; set; }
        public double? Money { get; set; }
        public string? Type { get; set; }
        public string? UserId { get; set; }
        public string? AdminId { get; set; }

        public virtual EcommerceAccount? AccountIdNumNavigation { get; set; }
        public virtual ApplicationUser? Admin { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
