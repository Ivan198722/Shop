using Shop.Models;

namespace Shop.ViewModels
{
    public class AdminViewModel
    {
       public IEnumerable<ProductInfo> Products { get; set; }

      // public IEnumerable<ProductInfo> FiltrProducts { get; set; }

       // public IEnumerable<Product> Products { get; set; }
        //public IEnumerable<ProductInfo> Categories {  get; set; }

        public IEnumerable<ProductInfo> UniqueProd {  get; set; }

        //public IEnumerable<Product> UniqueProd {  get; set; }
    }
}
