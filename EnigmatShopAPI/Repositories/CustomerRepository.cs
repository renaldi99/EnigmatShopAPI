using EnigmatShopAPI.Models;
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
            throw new NotImplementedException();
        }

        public Task<int> Delete(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Customer>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Customer>> FindAllAsync(Expression<Func<Customer, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public Task<List<Customer>> FindAllAsync(Expression<Func<Customer, bool>> criteria, string[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<Customer?> FindAsync(Expression<Func<Customer, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public Task<Customer?> FindAsync(Expression<Func<Customer, bool>> criteria, string[] includes)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
