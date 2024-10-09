using DomainLayerCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InfrastructureLayerEF.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDBContext _context;
        public GenericRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);

            return entities;
        }



        public T Find(Expression<Func<T, bool>> criateria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            return query.SingleOrDefault(criateria);
        }

        public IEnumerable<T> FindALL(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            // Apply the includes
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            // Apply the filter criteria
            return query.Where(criteria).ToList();
        }



        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetByID(int id)
        {
            var product = _context.Set<T>().Find(id);
            return product;// _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIDASync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public int Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Complete();
        }
        public int Delete(int id)
        {
            T entity = GetByID(id);
            _context.Set<T>().Remove(entity);
            return Complete();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> criateria)
        {
            return _context.Set<T>().Count(criateria);
        }

        
    }
}
