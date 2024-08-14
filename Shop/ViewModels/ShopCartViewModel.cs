using Shop.Models;

namespace Shop.ViewModels
{
    public class ShopCartViewModel
    {
        public ShopCart shopCart { get; set; }

        public decimal totalPrice { get; set; }
    }
}
