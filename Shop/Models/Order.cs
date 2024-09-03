using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Order
    {
        public int Id { get; set; }

        
        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }

        
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderTime { get; set; }

        public bool PaymentStatus { get; set; }

     
        public virtual FinishedOrder FinishedOrder { get; set; }

    }
}
