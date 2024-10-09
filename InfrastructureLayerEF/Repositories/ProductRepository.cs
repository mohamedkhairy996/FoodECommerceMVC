using DomainLayerCore.Interfaces;
using DomainLayerCore.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace InfrastructureLayerEF.Repositories
{
    public class ProductRepository : GenericRepository<Product> , IProductRepository
    {
        private readonly ApplicationDBContext _context;

        public ProductRepository(ApplicationDBContext context):base(context) 
        {
            _context = context;
        }
        public List<Product> GetProductsByName(string searchName)
        {
            return _context.products
                .Where(p => p.Name.Contains(searchName))
                .ToList();
        }

        public List<Product> GetAllWithCategory()
        {
            return _context.products.Include(p => p.category).ToList();
        }
        public Product GetByIdWithCategory(int? id)
        {
            if (id == 0) { return new Product(); }
            else { return _context.products.Include(x => x.category).FirstOrDefault(x => x.Id == id); }
        }
        public Product GetByHomeImage(string homeUrl)
        {
            return _context.products.FirstOrDefault(i => i.HomeImageUrl == homeUrl);
        }
        
        public bool Add(Product product)
        {
            if (product != null)
            {
                _context.products.Add(product);
                _context.SaveChanges();
                return true;
            }
            else { return false; }
        }
        public void Edit(int id, Product product)
        {
            if (id != 0)
            {
                var oldProduct = GetByID(id);
                oldProduct.Name = product.Name;
                oldProduct.Description = product.Description;
                oldProduct.Price = product.Price;
                oldProduct.ImageUrls = product.ImageUrls;
                oldProduct.HomeImageUrl = product.HomeImageUrl;
                oldProduct.CategoryId = product.CategoryId;

                _context.SaveChanges();

            }
            else { }
        }
        public bool Delete(int id)
        {
            if (id != 0)
            {
                var oldProduct = GetByID(id);
                _context.products.Remove(oldProduct);
                _context.SaveChanges();
                return true;
            }
            else { return false; }
        }

        public bool IsExist(string Name)
        {
            return _context.products.Any(c => c.Name.Equals(Name));
        }
    }
}
