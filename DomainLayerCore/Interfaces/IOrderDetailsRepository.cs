using DomainLayerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayerCore.Interfaces
{
    public interface IOrderDetailsRepository : IGenericRepository<OrderDetails>
    {
        List<OrderDetails> GetOrderDetailsIncludeProductsByHeaderId(int headerId);
    }
}
