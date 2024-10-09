using System.ComponentModel.DataAnnotations;

namespace DomainLayerCore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Length must be less than 30 char")]
        public string Name { get; set; }


    }
}
