using Shop.Models;

namespace Shop.Interfaces
{
    public interface IAllProducts
    {

        Task<IEnumerable<ProductDetails>> GetProductDetailsAsync();
    }
}
