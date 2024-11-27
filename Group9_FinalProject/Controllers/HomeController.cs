using Group9_FinalProject.Data;
using Group9_FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Group9_FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; // Logger for logging information or errors
        private readonly ApplicationDbContext _context; // Database context for interacting with the database

        // Constructor to inject ApplicationDbContext and ILogger
        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context; // Assigning injected database context
            _logger = logger; // Assigning injected logger
        }

        // Privacy page action
        // This action renders the Privacy page (static content).
        public IActionResult Privacy()
        {
            return View(); // Return the Privacy view
        }

        // Error handling action
        // This action is used to display error information when exceptions occur.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Return the Error view with the RequestId for debugging purposes
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: Home page
        // This action fetches all products from the database, grouped by their categories,
        // and displays them on the home page in a categorized format.
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Group products by category and fetch the data from the database
            var productsByCategory = await _context.Products
                .GroupBy(p => p.Category) // Group products by Category
                .Select(group => new
                {
                    Category = group.Key, // The category name
                    Products = group.ToList() // List of products in this category
                }).ToListAsync();

            // Pass the grouped products to the view
            return View(productsByCategory);
        }

        // GET: Product Details
        // This action fetches the details of a specific product by its ID.
        // It renders a detailed view of the product, including its name, price, and description.
        [HttpGet]
        public async Task<IActionResult> ProductDetails(int id)
        {
            // Fetch the product by its ID
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                // Return a 404 Not Found response if the product doesn't exist
                return NotFound();
            }

            // Pass the product details to the view
            return View(product);
        }
    }
}
