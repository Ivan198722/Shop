using Shop.Models;

namespace Shop.Interfaces
{
    public interface IAllSale
    {
        Task<int> GetCountActiveOrders();

        Task<IEnumerable<OrderInfo>> GetActiveOrders();

        Task<Company> GetDataCompany();

        Task FinishOrder(int orderId);

        Task<IEnumerable<PrintOrderDetail>> GetProductsSold(string sort);

        Task<IEnumerable<PrintCustomer>> GetCustomers(string sort);
        Task<IEnumerable<OrderInfo>> GetCustomerOrders(int customerId);
    }
}
