using DomainLayerCore.Models;

namespace DomainLayerCore.Interfaces
{
    public interface IUserOrderHeaderRepository : IGenericRepository<UserOrderHeader>
    {
        List<UserOrderHeader> GetOrderHeaderByStatus(string status);

        List<UserOrderHeader> GetUserOrderHeadersByStatus(string userId , string status);

        List<UserOrderHeader> GetUserOrderHeaders(string userId);

    }
}
