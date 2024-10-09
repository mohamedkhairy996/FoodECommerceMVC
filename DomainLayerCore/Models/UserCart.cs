using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayerCore.Models
{
    public class UserCart
    {
        [Key]
        public int Id { get; set; }

        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]

        public Product Product { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]

        public ApplicationUser User { get; set; }

        public int Quantity { get; set; }
        [NotMapped]
        public double QuantityPrice { get; set; }
    }
}
