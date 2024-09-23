namespace Shop.Models
{
    public class OrderInfo
    {
        public Customer Customer { get; set; }

        public Order Order { get; set; }

       // public IEnumerable<OrderDetail> Details { get; set; }
         public IEnumerable<PrintOrderDetail> Details { get; set; }

       // public IEnumerable<Product> Products { get; set; }  

        
    }

    public class PrintOrderDetail
    {
        public int Id { get; set; }

        public int orderId { get; set; }

        public int productID { get; set; }

        public Product Product { get; set; }
       // public string productName { get; set; }

        public int quantity { get; set; }

        public decimal price { get; set; }
    }

}
