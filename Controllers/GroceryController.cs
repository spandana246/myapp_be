using Microsoft.AspNetCore.Mvc;
using GroceryAppBackend.Models; // ✅ Add this line

namespace GroceryAppBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroceryController : ControllerBase
    {
        private static List<string> items = new() { "Apples", "Bananas", "Milk", "Bread" };

        [HttpGet]
        public IActionResult GetItems()
        {
            return Ok(items);
        }

        [HttpPost("add")]
        public IActionResult AddItem([FromBody] CartItem newItem)
        {
            if (string.IsNullOrEmpty(newItem.Name))
                return BadRequest("Item name is required");

            items.Add(newItem.Name);
            return Ok("Item added");
        }
    }
}
