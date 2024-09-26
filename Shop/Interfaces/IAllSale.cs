using Shop.Models;

namespace Shop.Interfaces
{
    public interface IAllSale
    {
        Task<int> GetCountActiveOrders();

        Task<IEnumerable<OrderInfo>> GetActiveOrders();

        Task<Company> GetDataCompany();

        Task EditCompany(string name, string city, string postcode, string street, string number, string phoneNumber,
            string NIP, string email);

        Task FinishOrder(int orderId);

        Task<IEnumerable<PrintOrderDetail>> GetProductsSold(string sort);

        Task<IEnumerable<PrintCustomer>> GetCustomers(string sort);
        Task<IEnumerable<OrderInfo>> GetCustomerOrders(int customerId);
    }
}
