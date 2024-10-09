using DomainLayerCore.Models;

namespace DomainLayerCore.ViewModels
{
    public class ProductVM
    {
        public List<Product> products { get; set; }

        public List<Category> categories { get; set; }

        public string? SearchName { get; set; }
    }
}
