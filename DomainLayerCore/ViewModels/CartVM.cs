using DomainLayerCore.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayerCore.ViewModels
{
    public class CartVM
    {
        public List<Product> Products = new List<Product>();

        public Dictionary<string, int> Quantity = new Dictionary<string, int>();

        public Dictionary<string, double> QuantityPrice = new Dictionary<string, double>();

    }
}
