using EnigmatShopAPI.Models;

namespace EnigmatShopAPI.Message
{
    public class PurchaseResponseModel
    {
        public bool is_success { get; set; }
        public string message { get; set; }
        public Guid customer_id { get; set; }
        public string transaction_date { get; set; }
        public List<PurchaseDetailsInfo>? purchase_details { get; set; }
    }

    public class PurchaseDetailsInfo
    {
        public string? product_id { get; set; }
        public int quantity { get; set; }
    }
}
