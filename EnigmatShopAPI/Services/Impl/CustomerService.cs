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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> DeleteCustomerById(string id)
        {
            var customer = await GetCustomerById(id);
            if (customer == null)
            {
                throw new NotFoundException("Customer Doesn't Exist");
            }

            var result = await _repository.Delete(customer);
            var response = await _persistence.SaveChangesAsync();

            return response;
            
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
                if (result is null)
                {
                    throw new Exception("Internal Server Error");
                }

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Customer> GetCustomerByName(string name)
        {
            var result = await _repository.FindAsync(customer => customer.CustomerName.Equals(name));
            if (result == null)
            {
                throw new NotFoundException($"Not found customer with name: {name}");
            }

            return result;
        }

        public async Task<int> UpdateCustomer(Customer entity)
        {
            try
            {
                var result = _repository?.Update(entity);
                var response = await _persistence.SaveChangesAsync();

                return response;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
