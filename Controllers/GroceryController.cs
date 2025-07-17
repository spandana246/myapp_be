using Microsoft.AspNetCore.Mvc;
using GroceryAppBackend.Models;

namespace GroceryAppBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroceryController : ControllerBase
    {
        private static List<CartItem> cartItems = new()
        {
            new CartItem { Id = 1, Name = "Apples", Quantity = 2 },
            new CartItem { Id = 2, Name = "Bananas", Quantity = 5 }
        };

        // GET /grocery
        [HttpGet]
        public IActionResult GetCart()
        {
            return Ok(cartItems);
        }

        // POST /grocery/add
        [HttpPost("add")]
        public IActionResult AddItem([FromBody] CartItem newItem)
        {
            if (string.IsNullOrWhiteSpace(newItem.Name))
                return BadRequest("Item name is required");

            newItem.Id = cartItems.Count > 0 ? cartItems.Max(x => x.Id) + 1 : 1;

            cartItems.Add(newItem);
            return Ok(newItem);
        }

        // DELETE /grocery/delete/{id}
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteItem(int id)
        {
            var item = cartItems.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound("Item not found");

            cartItems.Remove(item);
            return Ok("Item removed");
        }

        // PUT /grocery/update/{id}
        [HttpPut("update/{id}")]
        public IActionResult UpdateItem(int id, [FromBody] CartItem updatedItem)
        {
            var item = cartItems.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound("Item not found");

            item.Name = updatedItem.Name ?? item.Name;
            item.Quantity = updatedItem.Quantity;

            return Ok(item);
        }
    }
}
