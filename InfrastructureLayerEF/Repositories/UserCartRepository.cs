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
    public class UserCartRepository : GenericRepository<UserCart>, IUserCartRepository
    {
        private readonly ApplicationDBContext _context;

        public UserCartRepository(ApplicationDBContext context):base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserCart>> GetUserCartsAsync(string userId)
        {
            return await _context.UserCarts.Where(u => u.UserId.Contains(userId)).ToListAsync();
        }
        public IEnumerable<UserCart> GetUserCarts(string userId)
        {
            return  _context.UserCarts.Where(u => u.UserId.Contains(userId));
        }
        
        public IEnumerable<UserCart> GetUserCartIncludesProducts(string userId)
        {
            return _context.UserCarts.Include(x => x.Product).Where(x => x.UserId.Contains(userId)).ToList();
        }

        public UserCart GetUserCartForProduct(string userId , int pid)
        {
            return _context.UserCarts.FirstOrDefault(u => u.UserId.Contains(userId) && u.ProductId == pid);
        }
    }
}
