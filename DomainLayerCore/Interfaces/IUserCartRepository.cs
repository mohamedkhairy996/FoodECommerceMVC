using DomainLayerCore.Models;

namespace DomainLayerCore.Interfaces
{
    public interface IUserCartRepository : IGenericRepository<UserCart>
    {
        Task<IEnumerable<UserCart>> GetUserCartsAsync(string userId);

        IEnumerable<UserCart> GetUserCarts(string userId);

        UserCart GetUserCartForProduct(string userId, int pid);

        IEnumerable<UserCart> GetUserCartIncludesProducts(string userId);
    }
}
