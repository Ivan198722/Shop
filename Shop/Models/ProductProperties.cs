using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class ProductProperties
    {
        public int Id { get; set; }

        public int? productId { get; set; }

        public int categoryId { get; set; }

        public string propertyName {  get; set; }

        public string? propertyParameters { get; set; }

        public virtual Category Category { get; set; }

        public virtual Product Product { get; set; }


        [NotMapped]
        public bool IsMatchingProperty { get; set; }
    }
}
