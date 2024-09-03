using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class ShopCartItem
    {
        public int Id { get; set; }

        public Product product { get; set; }

        public decimal price { get; set; }

        public int quantity { get; set; }
        
        public string ShopCartId { get; set; }

        public Images? images { get; set; }
    }
}
