using Group9_FinalProject.Data;
using Group9_FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Group9_FinalProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject ApplicationDbContext for database operations
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Login Page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Handle Admin Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Validate admin credentials
                var admin = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == model.Username && u.Role == "Admin");

                if (admin != null && BCrypt.Net.BCrypt.Verify(model.Password, admin.PasswordHash))
                {
                    // Store admin username in session
                    HttpContext.Session.SetString("AdminUsername", admin.Username);
                    return RedirectToAction("Products", "Admin");
                }

                // Add error message for invalid credentials
                ModelState.AddModelError("", "Invalid username or password");
            }

            return View(model);
        }

        // GET: Admin Dashboard
        [HttpGet]
        public IActionResult Dashboard()
        {
            // Redirect to login if admin is not authenticated
            if (HttpContext.Session.GetString("AdminUsername") == null)
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        // GET: Logout Admin
        [HttpGet]
        public IActionResult Logout()
        {
            // Clear session on logout
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // GET: List of Products for Admin
        [HttpGet]
        public async Task<IActionResult> Products()
        {
            // Ensure admin is authenticated
            if (HttpContext.Session.GetString("AdminUsername") == null)
            {
                return RedirectToAction("Login");
            }

            // Fetch all products from database
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        // GET: Add Product Page
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        // POST: Add New Product
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product model, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                // Save uploaded image if provided
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Save file to server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }

                    // Set image URL in the product model
                    model.ImageURL = "/images/" + uniqueFileName;
                }

                // Add product to database
                _context.Products.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Products", "Admin");
            }

            return View(model);
        }

        // GET: Edit Product Page
        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            // Fetch product by ID
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Update Product Details
        [HttpPost]
        public async Task<IActionResult> EditProduct(int id, Product model, IFormFile? ImageFile)
        {
            if (id != model.ProductID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                // Fetch existing product from database
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                // Update product details
                product.Name = model.Name;
                product.Category = model.Category;
                product.Price = model.Price;
                product.StockQuantity = model.StockQuantity;
                product.Description = model.Description;

                // Save new image if provided
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }

                    product.ImageURL = "/images/" + uniqueFileName;
                }

                // Save changes to database
                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return RedirectToAction("Products", "Admin");
            }

            return View(model);
        }

        // GET: Delete Product
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // Fetch product by ID
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // Delete image file if it exists
            if (!string.IsNullOrEmpty(product.ImageURL))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.ImageURL.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            // Remove product from database
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Products");
        }
    }
}
