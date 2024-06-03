namespace Shop.Models
{
    public class ProductInfo
    {
        public Category Category { get; set; }

        public IEnumerable<Product> products { get; set; }

        public IEnumerable<ProductHighlights> highlights { get; set; }

        public IEnumerable<Image> images { get; set; }  

        public IEnumerable<ProductProperties> productProperties { get; set; }

        
    }
}
