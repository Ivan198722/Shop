using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class ProductHighlights
    {
        public int Id { get; set; }

        public string name { get; set; }

        public int productId { get; set; }

      

        public Product Product { get; set; }

        

        [NotMapped]
        public bool IsMatchingHighlights { get; set; }
    }
}
