using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EnigmatShopAPI.Dto
{
    public class UserDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Email { get; set; }
        [JsonIgnore]
        public string? Role { get; set; }
        [JsonIgnore]
        public string? RefreshToken { get; set; }
    }
}
