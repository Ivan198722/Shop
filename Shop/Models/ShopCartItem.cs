using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class ShopCartItem
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        public int price { get; set; }

        public int quantity { get; set; }
        
        public string ShopCartId { get; set; }
    }
}
