using DomainLayerCore.Models;
using Microsoft.AspNetCore.Http;

namespace DomainLayerCore.ViewModels
{
    public class ProductViewModel
    {
        public Product? Product { get; set; }

        public string? SearchItem { get; set; }

        public List<Category>? CategoriesList { get; set; }

        public IEnumerable<IFormFile>? Images { get; set; }

        public Inventory? Inventories { get; set; }

        public PImages? pImages { get; set; }
    }
}
