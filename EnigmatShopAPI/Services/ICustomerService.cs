using EnigmatShopAPI.Models;

namespace EnigmatShopAPI.Services
{
    public interface ICustomerService
    {
        Task<int> CreateCustomer(Customer entity);
        Task<Customer> GetCustomerById(string id);
        Task<List<Customer>> GetAllCustomer();
        Task<Customer> UpdateCustomer(Customer entity);
        Task<int> DeleteCustomerById(string id);
    }
}
