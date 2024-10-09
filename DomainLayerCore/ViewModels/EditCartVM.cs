using DomainLayerCore.Models;

namespace DomainLayerCore.ViewModels
{
    public class EditCartVM
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public double QuantityPrice { get; set; }
    }
}
