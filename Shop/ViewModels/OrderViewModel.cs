using Shop.Models;

namespace Shop.ViewModels
{
    public class OrderViewModel
    {
        public Customer Customer { get; set; }

        public string Email { get; set; }

        public Customer OrderCustomer {  get; set; }

        public ShopCart ShopCart { get; set; }

        public decimal totalPrice { get; set; }
    }
}
