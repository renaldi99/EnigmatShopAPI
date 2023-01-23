using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnigmatShopAPI.Models
{
    [Table(name: "ES_Customer_TM")]
    public class Customer
    {
        [Key, Column(name: "id")]
        public Guid Id { get; set; }
        [Required, Column(name: "customer_name")]
        public string? CustomerName { get; set; }
        [Required, Column(name: "email")]
        public string? Email { get; set; }
        [Required, Column(name: "nomor_hp")]
        public string? NomorHp { get; set; }
    }
}
