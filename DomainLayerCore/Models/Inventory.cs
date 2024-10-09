using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayerCore.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 999, ErrorMessage = "Range from 1 tp 999.99")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Please insert two digits after decimal ex:- 0.00")]
        public double PurchasePrice { get; set; }

        public string Category { get; set; }

        public int Quantity { get; set; }

    }
}
