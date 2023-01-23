using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnigmatShopAPI.Models
{
    [Table(name: "ES_PurchaseDetail_TM")]
    public class PurchaseDetail
    {

        [Key, Column(name: "id")]
        public Guid Id { get; set; }
        [Required, Column(name: "purchase_id")]
        public Guid PurchaseId { get; set; }
        [Required, Column(name: "product_id")]
        public Guid ProductId { get; set; }
        [Required, Column(name: "quantity")]
        public int Quantity { get; set; }

        public virtual Purchase? Purchase { get; set; }
        public virtual Product? Product { get; set; }

    }
}
