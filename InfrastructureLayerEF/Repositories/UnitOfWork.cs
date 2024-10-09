
using DomainLayerCore.Interfaces;
using DomainLayerCore.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Concurrent;
namespace InfrastructureLayerEF.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
         
        public IUserCartRepository UserCarts { get; private set; }

        public IUserOrderHeaderRepository UserOrderHeaders { get; private set; }

        public IGenericRepository<Inventory> Inventories { get; private set; }

        public IOrderDetailsRepository OrdersDetails { get; private set; }

        public IPImagesRepository PImages { get; private set; }

        public IGenericRepository<Transiaction> Transiactions { get; private set; }

        public IGenericRepository<EcommerceAccount> EcommrceAccounts { get; private set; }

        public IApplicationUserRepository ApplicationUsers { get; private set; }

        public IGenericRepository<IdentityRole> Roles  { get; private set; }

        public IGenericRepository<Message> Messages  { get; private set; }

        public IGenericRepository<MessageHeader> MessageHeaders  { get; private set; }

        public IProductRepository Products { get; private set; }

        public ICategoryRepository Categories { get; private set; }

        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
            UserCarts = new UserCartRepository(_context);
            UserOrderHeaders = new UserOrderHeaderRepository(_context);
            Inventories = new GenericRepository<Inventory>(_context);
            OrdersDetails = new OrderDetailsRepository(_context);
            PImages = new PImagesRepository(_context);
            Transiactions = new GenericRepository<Transiaction>(_context);
            EcommrceAccounts = new GenericRepository<EcommerceAccount>(_context);
            ApplicationUsers = new ApplicationUserRepository(_context);
            Roles = new GenericRepository<IdentityRole>(_context);
            Messages = new GenericRepository<Message>(_context);
            MessageHeaders = new GenericRepository<MessageHeader>(_context);
            Products = new ProductRepository(_context);
            Categories = new CategoryRepository(_context);
        }

        public int ApplyChanges()
        {
            return _context.SaveChanges();
        }
        public async Task<int> ApplyChangesAsync() 
        {
           return await _context.SaveChangesAsync();
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
