namespace GroceryAppBackend.Models
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public List<CartItem> Cart { get; set; } = new();
    }
}
