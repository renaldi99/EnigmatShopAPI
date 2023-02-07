using EnigmatShopAPI.Models;
using EnigmatShopAPI.Repositories;

namespace EnigmatShopAPI.Services.Impl
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository? _repository;
        private readonly IPersistence? _persistence;

        public ProductService(IProductRepository? repository, IPersistence? persistence)
        {
            _repository = repository;
            _persistence = persistence;
        }

        public async Task<int> CreateProduct(Product entity)
        {
            var result = await _repository.SaveAsync(entity);
            if (result == null)
            {
                throw new Exception("Internal Server Error");
            }
            var response = await _persistence.SaveChangesAsync();
            return response;
        }

        public Task<int> DeleteProductById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllProduct()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateProduct(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
