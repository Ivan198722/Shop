namespace Shop.Models
{
    public class ProductInfo
    {
        public Category Category { get; set; }

        public IEnumerable<Product> products { get; set; }

        public IEnumerable<ProductHighlights> highlights { get; set; }

        public IEnumerable<Image> images { get; set; }  

        public IEnumerable<CellphoneProperties> cellphonesProperties { get; set; }

        public IEnumerable<TVProperties> tvProperties { get; set; }

        public IEnumerable<PhotoProperties> photoProperties { get; set; }
    }
}
