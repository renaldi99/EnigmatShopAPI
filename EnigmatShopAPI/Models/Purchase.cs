using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnigmatShopAPI.Models
{
    [Table(name: "ES_Purchase_TM")]
    public class Purchase
    {
        [Key, Column(name: "id")]
        public Guid Id { get; set; }
        [Column(name: "customer_id")]
        public Guid CustomerId { get; set; }
        [Column(name: "date")]
        public DateTime Date { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual List<PurchaseDetail>? PurchaseDetails { get; set; }
    }
}
