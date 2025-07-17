using Microsoft.AspNetCore.Mvc;
using GroceryAppBackend.Models;

namespace GroceryAppBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private static List<User> users = new List<User>();

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username and password are required");
            }

            if (users.Any(u => u.Username == request.Username))
            {
                return BadRequest("User already exists");
            }

            var newUser = new User
            {
                Username = request.Username,
                Password = request.Password,
                Cart = new List<CartItem>()
            };

            users.Add(newUser);
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = users.FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);
            if (user == null)
            {
                return BadRequest("Invalid username or password");
            }

            return Ok("Login successful");
        }

        [HttpGet("cart/{username}")]
        public IActionResult GetCart(string username)
        {
            var user = users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user.Cart);
        }

        [HttpPost("cart/{username}/add")]
        public IActionResult AddToCart(string username, [FromBody] CartItem newItem)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(newItem.Name))
                return BadRequest("Invalid input");

            var user = users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return NotFound("User not found");

            var cart = user.Cart;
            var existingItem = cart.FirstOrDefault(i => i.Name == newItem.Name);

            if (existingItem != null)
            {
                existingItem.Quantity += 1; // ✅ Increment quantity
            }
            else
            {
                newItem.Id = cart.Count > 0 ? cart.Max(i => i.Id) + 1 : 1;
                newItem.Quantity = 1; // ✅ Start at 1
                cart.Add(newItem);
            }

            return Ok(cart);
        }

        [HttpPost("cart/{username}/checkout")]
        public IActionResult Checkout(string username)
        {
            var user = users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return NotFound("User not found");

            user.Cart.Clear(); // ✅ Clear the cart on checkout
            return Ok("Checkout successful");
        }
    }
}
