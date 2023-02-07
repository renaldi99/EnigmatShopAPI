using EnigmatShopAPI.Models;

namespace EnigmatShopAPI.Services
{
    public interface ICustomerService
    {
        Task<int> CreateCustomer(Customer entity);
        Task<Customer> GetCustomerById(string id);
        Task<List<Customer>> GetAllCustomer();
        Task<int> UpdateCustomer(Customer entity);
        Task<int> DeleteCustomerById(string id);
        Task<Customer> GetCustomerByName(string name);
    }
}
