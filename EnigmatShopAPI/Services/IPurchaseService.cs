using EnigmatShopAPI.Message;
using EnigmatShopAPI.Models;

namespace EnigmatShopAPI.Services
{
    public interface IPurchaseService
    {
        Task<PurchaseResponseModel> CreatePurchase(Purchase entity);
        Task<Purchase> GetPurchaseById(string id);
        Task<List<Purchase>> GetAllPurchase();
        Task<int> UpdatePurchase(Purchase entity);
        Task<int> DeletePurchaseById(string id);
        Task<Purchase> GetPurchaseByName(string name);
    }
}
