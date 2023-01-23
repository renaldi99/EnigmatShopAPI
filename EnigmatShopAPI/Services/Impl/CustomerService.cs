using EnigmatShopAPI.Exceptions;
using EnigmatShopAPI.Models;
using EnigmatShopAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EnigmatShopAPI.Services.Impl
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository? _repository;
        private readonly IPersistence? _persistence;

        public CustomerService(ICustomerRepository? repository, IPersistence? persistance)
        {
            _repository = repository;
            _persistence = persistance;
        }

        public async Task<int> CreateCustomer(Customer entity)
        {
            try
            {
                var result = await _repository.SaveAsync(entity);
                var response = await _persistence.SaveChangesAsync();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<int> DeleteCustomerById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Customer>> GetAllCustomer()
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetCustomerById(string id)
        {
            try
            {
                var result = await _repository.FindByIdAsync(Guid.Parse(id));
                if (result is null) throw new NotFoundException("Not Found From Database");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Customer> UpdateCustomer(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
