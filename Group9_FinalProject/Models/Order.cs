using System.ComponentModel.DataAnnotations;

namespace Group9_FinalProject.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public string Status { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public string CustomerAddress { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
