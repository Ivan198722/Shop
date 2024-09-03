namespace Shop.Models
{
    public class FinishedOrder
    {
        public int Id { get; set; }

        public int orderId { get; set; }

        public DateTime orderTime { get; set; }

        public virtual Order Order { get; set; }
    }
}
