using DomainLayerCore.Interfaces;
using DomainLayerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayerEF.Repositories
{
    public class UserOrderHeaderRepository : GenericRepository<UserOrderHeader> , IUserOrderHeaderRepository
    {
        private readonly ApplicationDBContext _context;

        public UserOrderHeaderRepository(ApplicationDBContext context):base(context)
        {
            _context = context;
        }

        public List<UserOrderHeader> GetOrderHeaderByStatus(string status)
        {
            return _context.UserOrderHeaders.Where(u => u.OrderStatus == status).ToList();
        }

        public List<UserOrderHeader> GetUserOrderHeaders(string userId)
        {
            return _context.UserOrderHeaders.Where(u => u.UserId == userId).ToList();
        }

        public List<UserOrderHeader> GetUserOrderHeadersByStatus(string userId , string status)
        {
            return _context.UserOrderHeaders.Where(u => u.OrderStatus == status && u.UserId == userId).ToList();
        }
    }
}
