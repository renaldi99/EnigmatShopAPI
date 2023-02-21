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

        public Task<int> DeletePurchaseDetailById(string id)
        {
            throw new NotImplementedException();
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
