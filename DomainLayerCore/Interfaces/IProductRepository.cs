using DomainLayerCore.Models;
using DomainLayerCore.ViewModels;
using Microsoft.AspNetCore.Http;

namespace DomainLayerCore.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Product GetByHomeImage(string homeUrl);

        public List<Product> GetProductsByName(string searchName);

        public List<Product> GetAllWithCategory();

        public Product GetByIdWithCategory(int? id);
    }
}
