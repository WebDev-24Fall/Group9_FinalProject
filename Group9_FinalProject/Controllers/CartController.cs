using Group9_FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Group9_FinalProject.Data;
using Microsoft.EntityFrameworkCore;
using Group9_FinalProject.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace Group9_FinalProject.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly ApplicationDbContext _context;
        private const string CartSessionKey = "Cart";

        public CartController(ApplicationDbContext context, ILogger<CartController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Action to display the cart
        public IActionResult Index()
        {
            // Retrieve the cart from session
            var cart = HttpContext.Session.GetObjectFromJson<Cart>(CartSessionKey) ?? new Cart();
            return View("~/Views/Home/Cart.cshtml", cart);
        }

        // Action to add item to the cart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productID, int quantity)
        {
            // Fetch the product by ID
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductID == productID);

            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found.", productID);
                return NotFound();
            }

            // Get the cart from session or create a new one if it doesn't exist
            var cart = HttpContext.Session.GetObjectFromJson<Cart>(CartSessionKey) ?? new Cart();

            // Create a CartItem and add it to the cart
            var cartItem = new CartItem
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Price = product.Price,
                Quantity = quantity
            };

            cart.AddItem(cartItem);

            // Save the cart to session
            HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);

            return RedirectToAction("Index");
        }

        // Action to remove item from the cart
        [HttpPost]
        public IActionResult RemoveFromCart(int productID)
        {
            // Retrieve the cart from session
            var cart = HttpContext.Session.GetObjectFromJson<Cart>(CartSessionKey) ?? new Cart();

            // Remove the item from the cart
            cart.RemoveItem(productID);

            // Save the updated cart back to the session
            HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);

            return RedirectToAction("Index");
        }

        // Action to increase quantity of an item
        [HttpPost]
        public IActionResult IncreaseQuantity(int productID)
        {
            // Retrieve the cart from session
            var cart = HttpContext.Session.GetObjectFromJson<Cart>(CartSessionKey) ?? new Cart();

            // Find the item in the cart
            var item = cart.Items.FirstOrDefault(i => i.ProductID == productID);
            if (item != null)
            {
                item.Quantity++; // Increase quantity by 1
            }

            // Save the updated cart back to the session
            HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DecreaseQuantity(int productID)
        {
            // Retrieve the cart from session
            var cart = HttpContext.Session.GetObjectFromJson<Cart>(CartSessionKey) ?? new Cart();

            // Find the item in the cart
            var item = cart.Items.FirstOrDefault(i => i.ProductID == productID);

            if (item != null)
            {
                // If the quantity is greater than 1, decrement the quantity
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                }
                else
                {
                    // If the quantity is 1, remove the item from the cart
                    cart.RemoveItem(productID);
                }
            }

            // Save the updated cart to the session
            HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            // Retrieve the cart from session
            var cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart") ?? new Cart();

            // Pass the cart to the view
            return View("~/Views/Home/Checkout.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(string Name, string Address, string Phone)
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart");

            if (cart == null || !cart.Items.Any())
            {
                // If the cart is empty, redirect back to the cart page
                ModelState.AddModelError("", "Your cart is empty.");
                return RedirectToAction("Index");
            }

            // Simulate order creation (you can save it to the database if needed)
            var order = new Order
            {
                CustomerName = Name,
                CustomerAddress = Address,
                CustomerPhone = Phone,
                OrderDate = DateTime.Now,
                TotalAmount = cart.Total,
                Status = "Processing"
            };

            // Clear the cart
            HttpContext.Session.Remove("Cart");

            // Redirect to the Order Confirmation page
            return RedirectToAction("OrderConfirmation", new { customerName = Name });
        }
        [HttpGet]
        public IActionResult OrderConfirmation(string customerName)
        {
            ViewBag.CustomerName = customerName;
            return View("~/Views/Home/OrderConfirmation.cshtml");
        }
    }

}