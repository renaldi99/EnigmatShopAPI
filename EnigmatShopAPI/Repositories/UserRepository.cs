using EnigmatShopAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EnigmatShopAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public User Attach(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<User> Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> FindAllAsync(Expression<Func<User, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> FindAllAsync(Expression<Func<User, bool>> criteria, string[] includes)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> FindAsync(Expression<Func<User, bool>> criteria)
        {
            var result = await _appDbContext.Set<User>().Where(criteria).FirstOrDefaultAsync();
            return result;
        }

        public Task<User?> FindAsync(Expression<Func<User, bool>> criteria, string[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<User?> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> SaveAsync(User entity)
        {
            var result = await _appDbContext.Set<User>().AddAsync(entity);
            return result.Entity;
        }

        public User Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
