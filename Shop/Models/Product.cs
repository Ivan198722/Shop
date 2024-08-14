using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Shop.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string manufacturer { get; set; }
       
        public string name { get; set; }
       
        public decimal price { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime addDate { get; set; }
        
        public int quantity { get; set; }

        public string? shortDescription { get; set; }

        public string? longDescription { get; set; }

        public bool isFavorited { get; set; }

        public bool available { get; set; }

        public int categoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual IEnumerable<Images> Images { get; set; }

        public virtual IEnumerable<ProductHighlights> Highlights { get; set; }

        public virtual IEnumerable<ProductProperties> ProductProperties { get; set; }


        [NotMapped]
        public bool IsMatchingManufacturer { get; set; }
    }
}
