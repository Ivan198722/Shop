using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class CellphoneProperties
    {
        public int Id { get; set; }

        public int categoryId { get; set; }

        public float? screen {  get; set; }

        public string? color { get; set; }

        public int? memory {  get; set; }

        public int? ram { get; set; }

        public string? port { get; set; }

        public double? batteryCapacity { get; set; }

        public string? operatingSystem { get; set; }

       public virtual Category Category { get; set; }


        [NotMapped]
        public bool IsMatchingCellphone { get; set; }
    }
}
