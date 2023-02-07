using EnigmatShopAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EnigmatShopAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;

        public CustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Customer Attach(Customer entity)
        {
            var result = _appDbContext.Set<Customer>().Attach(entity);
            return result.Entity;
        }

        public async Task<Customer> Delete(Customer entity)
        {
            var result = _appDbContext.Set<Customer>().Remove(entity);
            return result.Entity; 
        }

        public async Task<List<Customer>> FindAllAsync()
        {
            var result = await _appDbContext.Set<Customer>().ToListAsync();
            return result;
        }

        public async Task<List<Customer>> FindAllAsync(Expression<Func<Customer, bool>> criteria)
        {
            var result = await _appDbContext.Set<Customer>().Where(criteria).ToListAsync();
            return result;
        }

        public async Task<List<Customer>> FindAllAsync(Expression<Func<Customer, bool>> criteria, string[] includes)
        {
            var query = _appDbContext.Set<Customer>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await _appDbContext.Set<Customer>().Where(criteria).ToListAsync();
        }

        public async Task<Customer?> FindAsync(Expression<Func<Customer, bool>> criteria)
        {
            var result = await _appDbContext.Set<Customer>().Where(criteria).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Customer?> FindAsync(Expression<Func<Customer, bool>> criteria, string[] includes)
        {
            var query = _appDbContext.Set<Customer>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await _appDbContext.Set<Customer>().Where(criteria).FirstOrDefaultAsync();
        }

        public async Task<Customer?> FindByIdAsync(Guid id)
        {
            var result = await _appDbContext.Set<Customer>().FindAsync(id);
            return result;
        }

        public async Task<Customer> SaveAsync(Customer entity)
        {
            var result = await _appDbContext.Set<Customer>().AddAsync(entity);
            return result.Entity;
        }

        public Customer Update(Customer entity)
        {
            Attach(entity);
            var result = _appDbContext.Set<Customer>().Update(entity);
            return result.Entity;
        }
    }
}
