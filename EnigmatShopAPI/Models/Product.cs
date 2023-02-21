using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnigmatShopAPI.Models
{
    [Table(name: "ES_Product_TM")]
    public class Product
    {
        [Key, Column(name: "id")]
        public Guid Id { get; set; }
        [Required, Column(name: "product_name")]
        public string? ProductName { get; set; }
        [Required, Column(name: "product_price")]
        public string? ProductPrice { get; set; }
        [Required, Column(name: "stock")]
        public int Stock { get; set; }
        [Column(name: "image", TypeName = "NVarchar(100)")]
        public string Image { get; set; }
    }
}
