using System.Collections.Generic;
using System.Linq;

namespace Group9_FinalProject.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public decimal Total => Items.Sum(item => item.Price * item.Quantity);

        // Adds an item to the cart, or updates the quantity if the item already exists
        public void AddItem(CartItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductID == item.ProductID);
            if (existingItem == null)
            {
                Items.Add(item);
            }
            else
            {
                existingItem.Quantity += item.Quantity;
            }
        }

        // Removes an item from the cart
        public void RemoveItem(int productID)
        {
            var item = Items.FirstOrDefault(i => i.ProductID == productID);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        // Clears the cart
        public void Clear()
        {
            Items.Clear();
        }
    }
}
