using DomainLayerCore.Models;

namespace DomainLayerCore.ViewModels
{
    public class OrderDetailsVM
    {
        public UserOrderHeader? OrderHeader { get; set; }

        public IEnumerable<OrderDetails>? UserProductList { get; set; }
    }
}
