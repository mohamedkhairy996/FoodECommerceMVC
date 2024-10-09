using DomainLayerCore.Models;
using DomainLayerCore.ViewModels;
using Microsoft.AspNetCore.Http;

namespace DomainLayerCore.Interfaces
{
    public interface IApplicationUserRepository : IGenericRepository<ApplicationUser>
    {
        ApplicationUser GetByID(String id);
    }
}
