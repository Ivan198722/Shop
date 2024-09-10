using Shop.Models;

namespace Shop.Interfaces
{
    public interface IAllOrders
    {

        Task<Customer> GetCustomer(string emailAddress);

        Task<int> CheckCustomer(int customerId, string email, string name, string surname, string city, string postcode,
             string street, string numberHouse, string numberFlat, string phone, string NIP);

        Task CompleteOrder(int customerId, string email, string name, string surname, string city, string postcode,
        string street, string numberHouse, string numberFlat, string phone, string NIP);

        public bool HasInsufficientItems { get; }
    }
       
}
