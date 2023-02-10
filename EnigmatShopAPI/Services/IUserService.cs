using EnigmatShopAPI.Models;

namespace EnigmatShopAPI.Services
{
    public interface IUserService
    {
        Task<int> CreateUser(User entity);
        Task<int> UpdateUser(User entity);
        Task<User> GetUserByUsername(string username);
        Task<User> Authenticate(string username, string password);
    }
}
