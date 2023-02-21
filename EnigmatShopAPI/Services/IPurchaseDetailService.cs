using EnigmatShopAPI.Models;

namespace EnigmatShopAPI.Services
{
    public interface IPurchaseDetailService
    {
        Task<int> CreatePurchaseDetail(PurchaseDetail entity);
        Task<PurchaseDetail> GetPurchaseDetailById(string id);
        Task<List<PurchaseDetail>> GetAllPurchaseDetail();
        Task<int> UpdatePurchaseDetail(PurchaseDetail entity);
        Task<int> DeletePurchaseDetailById(string id);
        Task<PurchaseDetail> GetPurchaseDetailByName(string name);
    }
}
