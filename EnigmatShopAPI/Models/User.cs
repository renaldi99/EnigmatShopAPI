using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnigmatShopAPI.Models
{
    [Table(name: "ES_User_TM")]
    public class User
    {
        [Key, Column(name:"id")]
        public int Id { get; set; }
        [Required, Column(name: "username", TypeName = "NVarchar(50)")]
        public string? Username { get; set; }
        [Required, Column(name: "email", TypeName = "NVarchar(50)")]
        public string? Email { get; set; }
        [Required, Column(name: "password", TypeName = "NVarchar(150)")]
        public string? Password { get; set; }
        [Required, Column(name: "role", TypeName = "NVarchar(10)")]
        public string? Role { get; set; }
        [Column("refresh_token")]
        public string? RefreshToken { get; set; }
    }
}
