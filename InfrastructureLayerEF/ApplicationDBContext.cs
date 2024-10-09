using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DomainLayerCore.Models;


namespace InfrastructureLayerEF
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDBContext() : base()
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Inventory> inventories { get; set; }
        public DbSet<PImages> pImages { get; set; }
        public DbSet<UserCart> UserCarts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageHeader> messageHeaders { get; set; }
        public virtual DbSet<EcommerceAccount> EcommerceAccounts { get; set; } = null!;
        public virtual DbSet<Transiaction> Transiactions { get; set; } = null!;
        public virtual DbSet<UserOrderHeader> UserOrderHeaders { get; set; } = null!;
        public virtual DbSet<OrderDetails> OrderDetails { get; set; } = null!;
    }
}
