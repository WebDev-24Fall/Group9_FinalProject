using Group9_FinalProject.Data;
using Group9_FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Group9_FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Group products by category for the index view
            var productsByCategory = await _context.Products
                .GroupBy(p => p.Category)
                .Select(group => new
                {
                    Category = group.Key,
                    Products = group.ToList()
                }).ToListAsync();

            return View(productsByCategory);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetails(int id)
        {
            // Fetch the product details by ID
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
            {
                // Log an error and return a 404 if the product is not found
                _logger.LogWarning("Product with ID {ProductId} not found.", id);
                return NotFound();
            }

            return View(product);
        }
    }
}
