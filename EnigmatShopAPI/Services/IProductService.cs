using EnigmatShopAPI.Models;

namespace EnigmatShopAPI.Services
{
    public interface IProductService
    {
        Task<int> CreateProduct(Product entity);
        Task<Product> GetProductById(string id);
        Task<List<Product>> GetAllProduct();
        Task<int> UpdateProduct(Product entity);
        Task<int> DeleteProductById(string id);
    }
}
