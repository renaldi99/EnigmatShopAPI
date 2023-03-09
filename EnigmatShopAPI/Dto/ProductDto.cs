using System.ComponentModel.DataAnnotations;

namespace EnigmatShopAPI.Dto
{
    public class ProductDto
    {
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public string? ProductPrice { get; set; }
        [Required]
        public int Stock { get; set; }
        public string? Image { get; set; }
        [Required]
        public IFormFile? ImageFile { get; set; }
    }

    public class UpdateProductDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public string? ProductPrice { get; set; }
        [Required]
        public int Stock { get; set; }
        public string? Image { get; set; }
        [Required]
        public IFormFile? ImageFile { get; set; }
    }
}
