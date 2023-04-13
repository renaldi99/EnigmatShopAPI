using EnigmatShopAPI.Models;
using EnigmatShopAPI.Repositories;

namespace EnigmatShopAPI.Services.Impl
{
    public class PurchaseDetailService : IPurchaseDetailService
    {
        private readonly IPersistence _persistence;
        private readonly IGenericRepository<PurchaseDetail> _repository;

        public PurchaseDetailService(IPersistence persistence, IGenericRepository<PurchaseDetail> repository)
        {
            _persistence = persistence;
            _repository = repository;
        }

        public async Task<int> CreatePurchaseDetail(PurchaseDetail entity)
        {
            var result = await _repository.SaveAsync(entity);
            var response = await _persistence.SaveChangesAsync();
            return response;
        }

        public async Task<int> DeletePurchaseDetailById(string id)
        {
            var result = await _repository.FindByIdAsync(Guid.Parse(id));
            var deleteResult = await _repository.Delete(result);
            var response = await _persistence.SaveChangesAsync();
            return response;

        }

        public Task<List<PurchaseDetail>> GetAllPurchaseDetail()
        {
            throw new NotImplementedException();
        }

        public Task<PurchaseDetail> GetPurchaseDetailById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<PurchaseDetail> GetPurchaseDetailByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatePurchaseDetail(PurchaseDetail entity)
        {
            throw new NotImplementedException();
        }
    }
}
