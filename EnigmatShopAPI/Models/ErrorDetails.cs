namespace EnigmatShopAPI.Models
{
    public class ErrorDetails
    {
        public int status_code { get; set; }
        public string? message { get; set; }
        public string? path { get; set; }
    }
}
