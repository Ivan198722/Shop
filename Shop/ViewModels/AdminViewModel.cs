using Shop.Models;

namespace Shop.ViewModels
{
    public class AdminViewModel
    {
        public IEnumerable<ProductInfo> Products { get; set; }

        public IEnumerable<ProductInfo> Categories {  get; set; }
    }
}
