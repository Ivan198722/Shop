using Shop.Models;

namespace Shop.Interfaces
{
    public interface IAllSale
    {
        Task<int> GetCountActiveOrders();

        Task<IEnumerable<OrderInfo>> GetActiveOrders();

        Task<Company> GetDataCompany();
    }
}
