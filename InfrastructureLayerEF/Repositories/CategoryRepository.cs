
using DomainLayerCore.Interfaces;
using DomainLayerCore.Models;
using System.Linq.Expressions;

namespace InfrastructureLayerEF.Repositories
{
    public class CategoryRepository : GenericRepository<Category> , ICategoryRepository
    {
        private readonly ApplicationDBContext _context;

        public CategoryRepository(ApplicationDBContext context):base(context) 
        {
            _context = context;
        }
        
        public Category GetById(int? id)
        {
            if (id == 0) { return new Category(); }
            else { return _context.Categories.FirstOrDefault(c => c.Id == id); }
        }
        public bool Add(Category category)
        {
            if (category != null)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return true;
            }
            else { return false; }
        }
        public void Edit(int id, Category category)
        {
            if (id != 0)
            {
                var oldCat = GetById(id);

                _context.SaveChanges();

            }
            else { }
        }
        public bool Delete(int id)
        {
            if (id != 0)
            {
                var oldCat = GetById(id);
                _context.Categories.Remove(oldCat);
                _context.SaveChanges();
                return true;
            }
            else { return false; }
        }

        public bool IsExist(string Name)
        {
            return _context.Categories.Any(c => c.Name.Equals(Name));
        }

    }
}
