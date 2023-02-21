using EnigmatShopAPI.Exceptions;
using EnigmatShopAPI.Models;
using EnigmatShopAPI.Repositories;

namespace EnigmatShopAPI.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IPersistence _persistence;

        public UserService(IGenericRepository<User> repository, IPersistence persistence)
        {
            _repository = repository;
            _persistence = persistence;
        }

        public Task<User> Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateUser(User entity)
        {
            var result = await _repository.SaveAsync(entity);
            var response = await _persistence.SaveChangesAsync();
            return response;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var result = await _repository.FindAsync(user => user.Username.Equals(username));
            return result;
        }

        public async Task<int> UpdateUser(User entity)
        {
            var result = _repository.Update(entity);
            var response = await _persistence.SaveChangesAsync();
            return response;
        }
    }
}
