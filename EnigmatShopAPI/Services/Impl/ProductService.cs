using EnigmatShopAPI.Exceptions;
using EnigmatShopAPI.Models;
using EnigmatShopAPI.Repositories;

namespace EnigmatShopAPI.Services.Impl
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product>? _repository;
        private readonly IPersistence? _persistence;

        public ProductService(IGenericRepository<Product>? repository, IPersistence? persistence)
        {
            _repository = repository;
            _persistence = persistence;
        }

        public async Task<int> CreateProduct(Product entity)
        {
            try
            {
                var result = await _repository.SaveAsync(entity);
                if (result == null)
                {
                    throw new Exception("Error Create Product");
                }
                var response = await _persistence.SaveChangesAsync();
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<int> DeleteProductById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllProduct()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductById(string id)
        {
            var result = await _repository.FindAsync(product => product.Id.Equals(Guid.Parse(id)));
            if (result == null)
            {
                throw new NotFoundException($"Product with id {id} doesn't exist");
            }

            return result;
        }

        public async Task<int> UpdateProduct(Product entity)
        {
            try
            {
                var _product = await _repository.FindByIdAsync(entity.Id);
                if (_product == null)
                {
                    throw new NotFoundException("Error Update Product");
                }

                _product.ProductName = entity.ProductName;
                _product.ProductPrice = entity.ProductPrice;
                _product.Stock = entity.Stock;
                _product.Image = entity.Image;

                _repository.Update(_product);
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
