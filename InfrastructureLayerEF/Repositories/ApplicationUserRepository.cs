using DomainLayerCore.Interfaces;
using DomainLayerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayerEF.Repositories
{
    public class ApplicationUserRepository : GenericRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDBContext _context;

        public ApplicationUserRepository(ApplicationDBContext context):base(context)
        {
            _context = context;
        }

        public ApplicationUser GetByID(string id)
        {
            return _context.ApplicationUser.FirstOrDefault(u => u.Id == id);
        }
    }
}
