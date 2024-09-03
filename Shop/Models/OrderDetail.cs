namespace Shop.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int orderId { get; set; }

        public int productID { get; set; }

        public int quantity { get; set; }

        public decimal price { get; set; }

        public virtual Product Product { get; set; }

        public virtual Order Order { get; set; }
    }
}
