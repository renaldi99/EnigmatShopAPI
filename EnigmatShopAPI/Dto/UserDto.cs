using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EnigmatShopAPI.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Email { get; set; }
        [JsonIgnore]
        public string? Role { get; set; }
        public string? RefreshToken { get; set; }
    }
}
