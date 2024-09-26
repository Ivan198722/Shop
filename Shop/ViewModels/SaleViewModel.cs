using Shop.Models;

namespace Shop.ViewModels
{
    public class SaleViewModel
    {
        public IEnumerable<OrderInfo> OrderInfo { get; set; }

        public IEnumerable<PrintOrderDetail> OrderDetail { get; set; }

        public IEnumerable<PrintCustomer> Customers { get; set; }

        public IEnumerable<OrderInfo> CustomerOrder { get; set; }
    }
}
