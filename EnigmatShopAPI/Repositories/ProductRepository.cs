using EnigmatShopAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EnigmatShopAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext? _appDbContext;

        public ProductRepository(AppDbContext? appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public  Product Attach(Product entity)
        {
            var result = _appDbContext.Set<Product>().Attach(entity);
            return result.Entity;
        }

        public Task<Product> Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> FindAllAsync(Expression<Func<Product, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> FindAllAsync(Expression<Func<Product, bool>> criteria, string[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> FindAsync(Expression<Func<Product, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> FindAsync(Expression<Func<Product, bool>> criteria, string[] includes)
        {
            throw new NotImplementedException();
        }

        public async Task<Product?> FindByIdAsync(Guid id)
        {
            return await _appDbContext.Set<Product>().FindAsync(id);
        }

        public async Task<Product> SaveAsync(Product entity)
        {
            var result = await _appDbContext.Set<Product>().AddAsync(entity);
            return result.Entity;
        }

        public Product Update(Product entity)
        {
            Attach(entity);
            var result = _appDbContext.Set<Product>().Update(entity);
            return result.Entity;
        }
    }
}
