using EnigmatShopAPI.Exceptions;
using EnigmatShopAPI.Models;
using EnigmatShopAPI.Repositories;

namespace EnigmatShopAPI.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPersistence _persistence;

        public UserService(IUserRepository repository, IPersistence persistence)
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
    }
}
