using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class TVProperties
    {
        public int Id { get; set; }

        public int productId { get; set; }

        public float? screen { get; set; }

        public string? color { get; set; }

        public string? port { get; set; }

        public string? energyClasse { get; set; }

        public string? screenResolution { get; set; }

        public string? pixelResolution { get; set; }

        public int? refreshRate { get; set; }

        public virtual Product Product { get; set; }


        [NotMapped]
        public bool IsMatchingTV { get; set; }
    }
}
