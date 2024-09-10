namespace Shop.Models
{
    public class OrderInfo
    {
        public Customer Customer { get; set; }

        public Order Order { get; set; }

        public IEnumerable<OrderDetail> Details { get; set; }

        public IEnumerable<Product> Products { get; set; }  

        
    }
}
