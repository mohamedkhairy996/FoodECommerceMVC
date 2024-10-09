using DomainLayerCore.Interfaces;
using DomainLayerCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayerEF.Repositories
{
    public class OrderDetailsRepository : GenericRepository<OrderDetails> , IOrderDetailsRepository
    {
        private readonly ApplicationDBContext _context;

        public OrderDetailsRepository(ApplicationDBContext context):base(context)
        {
            _context = context;
        }

        public List<OrderDetails> GetOrderDetailsIncludeProductsByHeaderId(int headerId)
        {
            return _context.OrderDetails.Include(x => x.Product).Where(x => x.OrderHeaderId == headerId).ToList();
        }
    }
}
