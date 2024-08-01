using Shop.Models;

namespace Shop.ViewModels
{
    public class ProductsViewModel
    {
        public IEnumerable<ProductDetails> Products { get; set; }
     
        public IEnumerable<ProductDetails> Highlights { get; set; }



        public int TotalPages { get; set; } 
        public int CurrentPage { get; set; } 
        public int CategoryId { get; set; }

        public IEnumerable<ProductInfo> UniqueProd { get; set; }
       
        public IEnumerable<PropertyInfo> UniqueProperty { get; set; }

        public IEnumerable <ProductProperties> PropertyParameters { get; set; }
    }
}
