﻿using EnigmatShopAPI.Exceptions;
using EnigmatShopAPI.Message;
using EnigmatShopAPI.Models;
using EnigmatShopAPI.Repositories;

namespace EnigmatShopAPI.Services.Impl
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPersistence _persistence;
        private readonly IGenericRepository<Purchase> _repository;
        private readonly IPurchaseDetailService _purchaseDetailService;
        private readonly IProductService _productService;

        public PurchaseService(IPersistence persistence, IGenericRepository<Purchase> repository, IPurchaseDetailService purchaseDetailService, IProductService productService)
        {
            _persistence = persistence;
            _repository = repository;
            _purchaseDetailService = purchaseDetailService;
            _productService = productService;
        }

        public async Task<PurchaseResponseModel> CreatePurchase(Purchase entity)
        {
            await _persistence.BeginTransactionAsync();
            try
            {
                entity.Date = DateTime.Now;
                var savePurchase = await _repository.SaveAsync(entity);
                await _persistence.SaveChangesAsync();


                foreach (var purchaseDetail in entity.PurchaseDetails)
                {
                    // kurangin stock produk / update
                    var product = await _productService.GetProductById(purchaseDetail.ProductId.ToString());

                    if (product.Stock < purchaseDetail.Quantity)
                    {

                        var pDetail = new PurchaseDetailsInfo
                        {
                            product_id = product.Id.ToString(),
                            quantity = purchaseDetail.Quantity,
                        }; 

                        return new PurchaseResponseModel
                        {
                            is_success = false,
                            message = $"Product {product.ProductName} stock is less than quantity",
                            customer_id = entity.CustomerId,
                            transaction_date = DateTime.Now.ToString("yyyyMMdd"),
                            purchase_details = new List<PurchaseDetailsInfo> { pDetail}

                        };
                    } else
                    {
                        product.Stock -= purchaseDetail.Quantity;
                    }
                }

                // !IMPORTANT: saat segala proses update atau perubahan perlu di save sekali lagi
                await _persistence.SaveChangesAsync();
                await _persistence.CommitTransactionAsync();

                var purchaseDetailsResponse = entity.PurchaseDetails.Select(x =>
                new PurchaseDetailsInfo {
                    product_id = x.ProductId.ToString(),
                    quantity = x.Quantity,
                }).ToList();

                var response = new PurchaseResponseModel
                {
                    is_success = true,
                    message = "Success Transaction",
                    customer_id = entity.CustomerId,
                    transaction_date = DateTime.Now.ToString("yyyyMMdd"),
                    purchase_details = purchaseDetailsResponse
                };

                return response;
                
            }
            catch (Exception)
            {
                await _persistence.RollbackTransactionAsync();
                throw new Exception("Internal Service Error");
            }
            
        }

        public async Task<int> DeletePurchaseById(string id)
        {
            var result = await _repository.FindAsync(pur => pur.Id.Equals(Guid.Parse(id)));
            var deleteResult = await _repository.Delete(result);
            var response = await _persistence.SaveChangesAsync();
            return response;
        }

        public Task<List<Purchase>> GetAllPurchase()
        {
            throw new NotImplementedException();
        }

        public async Task<Purchase> GetPurchaseById(string id)
        {
            var result = await _repository.FindAsync(pur => pur.Id.Equals(Guid.Parse(id)));
            if (result == null)
            {
                throw new NotFoundException($"Purchase with id {id} not exist");
            }

            return result;
        }

        public Task<Purchase> GetPurchaseByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdatePurchase(Purchase entity)
        {
            var result = _repository.Update(entity);
            var response = await _persistence.SaveChangesAsync();
            return response;
        }
    }
}
