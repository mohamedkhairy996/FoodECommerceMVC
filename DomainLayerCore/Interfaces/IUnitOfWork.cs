using DomainLayerCore.Models;
using Microsoft.AspNetCore.Identity;

namespace DomainLayerCore.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserCartRepository UserCarts { get; }
        IUserOrderHeaderRepository UserOrderHeaders { get; }
        IGenericRepository<Inventory> Inventories { get; }
        IOrderDetailsRepository OrdersDetails { get; }
        IGenericRepository<Transiaction> Transiactions { get; }
        IGenericRepository<EcommerceAccount> EcommrceAccounts { get; }
        IApplicationUserRepository ApplicationUsers { get; }
        IGenericRepository<IdentityRole> Roles { get; }
        IGenericRepository<Message> Messages { get; }
        IGenericRepository<MessageHeader> MessageHeaders { get; }

        IPImagesRepository PImages { get; }
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }



        int ApplyChanges();
        Task<int> ApplyChangesAsync();

    }
}
