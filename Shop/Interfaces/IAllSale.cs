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
    }
}
