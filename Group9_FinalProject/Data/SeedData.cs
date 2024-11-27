using Group9_FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace Group9_FinalProject.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
                    Role = "Admin"
                });
                context.Users.Add(new User
                {
                    Username = "ygu4492",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("8904492"),
                    Role = "Admin"
                });
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Apple",
                        Category = "Fruits",
                        Price = 1.2M,
                        StockQuantity = 100,
                        Description = "Fresh apples from the farm",
                        ImageURL = "/images/apple.jpg"
                    },
                    new Product
                    {
                        Name = "Milk",
                        Category = "Dairy",
                        Price = 3.5M,
                        StockQuantity = 50,
                        Description = "1-liter organic milk",
                        ImageURL = "/images/milk.jpg"
                    }
                );
            }

            context.SaveChanges();
        }
    }
}
