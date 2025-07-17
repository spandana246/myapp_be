namespace GroceryAppBackend.Models
{
    public class CartItem
    {
        public int Id { get; set; }              // Unique ID for each item
        public string Name { get; set; } = "";   // Item name
        public int Quantity { get; set; }        // Quantity of the item
    }
}
