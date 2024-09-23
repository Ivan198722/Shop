namespace Shop.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int orderId { get; set; }

        public int productID { get; set; }

        public bool IsReturned { get; set; }

        public virtual Product Product { get; set; }

        public virtual Order Order { get; set; }
    }
}
