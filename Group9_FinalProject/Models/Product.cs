using System.ComponentModel.DataAnnotations;

namespace Group9_FinalProject.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Description { get; set; }
        public string? ImageURL { get; set; }
    }

}
