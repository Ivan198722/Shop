using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string manufacturer { get; set; }

        public string name { get; set; }

        public ushort price { get; set; }
        
        public int? quantity { get; set; }

        public string? shortDescription { get; set; }

        public string? longDescription { get; set; }

        public int categoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual IEnumerable<Image> Images { get; set; }

        public virtual IEnumerable<ProductHighlights> Highlights { get; set; }

        

        //[NotMapped]
        //public bool IsMatchingManufacturer { get; set; }
    }
}
