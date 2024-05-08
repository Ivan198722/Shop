using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class PhotoProperties
    {
        public int Id { get; set; }

        public int categoryId { get; set; }

        public string? color { get; set; }

        public string? port { get; set; }

        public virtual Category Category {  get; set; } 

        [NotMapped]
        public bool IsMatchingPhoto { get; set; }
    }
}
