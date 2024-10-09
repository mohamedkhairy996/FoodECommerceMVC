using System.Linq.Expressions;


namespace DomainLayerCore.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetByID(int id);

        List<T> GetAll();

        Task<T> GetByIDASync(int id);

        T Find(Expression<Func<T, bool>> criateria, string[] includes = null);

        IEnumerable<T> FindALL(Expression<Func<T, bool>> criateria, string[] includes = null);

        T Add(T entity);
        
        Task<T> AddAsync(T entity);

        IEnumerable<T> AddRange(IEnumerable<T> entities);

        T Update(T entity);

        int Complete();
         
        Task<int> CompleteAsync();

        int Delete(T entity);

        int Delete(int Id);

        void DeleteRange(IEnumerable<T> entities);

        void Attach(T entity);

        int Count();

        int Count(Expression<Func<T, bool>> criateria);





    }
}
