using EnigmatShopAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace EnigmatShopAPI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public T Attach(T entity)
        {
            var result = _appDbContext.Set<T>().Attach(entity);
            return result.Entity;
        }

        public async Task<T> Delete(T entity)
        {
            var result = _appDbContext.Set<T>().Remove(entity);
            return result.Entity;
        }

        public async Task<List<T>> FindAllAsync()
        {
            var result = await _appDbContext.Set<T>().ToListAsync();
            return result;
        }

        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> criteria)
        {
            var result = await _appDbContext.Set<T>().ToListAsync();
            return result;
        }

        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes)
        {
            var query = _appDbContext.Set<T>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await _appDbContext.Set<T>().Where(criteria).ToListAsync();
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> criteria)
        {
            var result = await _appDbContext.Set<T>().Where(criteria).FirstOrDefaultAsync();
            return result;
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> criteria, string[] includes)
        {
            var query = _appDbContext.Set<T>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await _appDbContext.Set<T>().Where(criteria).FirstOrDefaultAsync();
        }

        public async Task<T?> FindByIdAsync(Guid id)
        {
            var result = await _appDbContext.Set<T>().FindAsync(id);
            return result;
        }

        public async Task<T> SaveAsync(T entity)
        {
            var result = await _appDbContext.Set<T>().AddAsync(entity);
            return result.Entity;
        }

        public T Update(T entity)
        {
            Attach(entity);
            var result = _appDbContext.Set<T>().Update(entity);
            return result.Entity;
        }
    }
}
