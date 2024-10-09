using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayerCore.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Length can not be over than 200 char")]
        public string Description { get; set; }

        [Required]
        [Range(1, 999, ErrorMessage = "Range from 1 tp 999.99")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Please insert two digits after decimal ex:- 0.00")]
        public double Price { get; set; }

        public ICollection<PImages>? ImageUrls { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? category { get; set; }

        public string? HomeImageUrl { get; set; }
    }
}
