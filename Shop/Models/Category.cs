using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string name { get; set; }

        public string? description { get; set; }

         public virtual IEnumerable<Product> products { get; set; }

        public virtual IEnumerable<ProductProperties> productProperties { get; set; }



        [NotMapped]
        public bool IsMatchingCategory{ get; set; }
    }
}
