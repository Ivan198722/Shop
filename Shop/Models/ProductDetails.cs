namespace Shop.Models
{
    public class ProductDetails
    {
        public Product Product { get; set; }

        public IEnumerable<ProductHighlights> highlights { get; set; }

        public IEnumerable<Images> images { get; set; }

        public IEnumerable<ProductProperties> productProperties { get; set; }

    }
}
