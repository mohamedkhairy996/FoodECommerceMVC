using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayerCore.Models
{
    public class PImages
    {
        [Required]
        public int Id { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product product { get; set; }

        public string ImageUrl { get; set; }

        public string ProductName { get; set; }





    }
}
