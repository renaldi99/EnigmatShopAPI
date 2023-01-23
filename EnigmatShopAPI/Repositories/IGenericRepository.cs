using System.Linq.Expressions;

namespace EnigmatShopAPI.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> SaveAsync(T entity);
        T Attach(T entity);
        Task<T?> FindByIdAsync(Guid id);
        Task<T?> FindAsync(Expression<Func<T, bool>> criteria);
        Task<T?> FindAsync(Expression<Func<T, bool>> criteria, string[] includes);
        Task<List<T>> FindAllAsync();
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> criteria);
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes);
        T Update(T entity);
        Task<int> Delete(T entity);

    }
}
